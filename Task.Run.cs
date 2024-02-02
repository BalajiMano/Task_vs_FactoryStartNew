public class TaskRun
{
    /// <summary>
    /// Task.Run is the simplified version of Task.Factory.StartNew().
    /// Unlike factory, Task.Run will offload the task to ThreadPool task scheduler. So the current UI thread TaskScheduler will be freed from the workload.
    /// Since it is executing from threadpool thread, .Net Core will not allow us to attach any child task.
    /// </summary>
    public async void Start()
    {
        ///Cancellation Token in case if we want to cancel the task in midway. 
        ///This can be handy when task takes time time to respond the user request.
        ///Use Case - Downloading files from internet
        CancellationTokenSource CanToken = new CancellationTokenSource();
        var task = Task.Run(async () =>
        {
            await Task.Delay(1000);
            var numbers = Enumerable.Range(10000, 1000000).ToList();
            return CalculateSumOfSquare(numbers);
        }, CanToken.Token);

        await Task.Delay(2000);
        Console.WriteLine($"We are Cancelling the task using token");
        ///We are cancelling after waiting for 2 seconds. This can happen in real life where user may want to cancel the task by selecting cancel.
        ///We may face this in WebApi.
        CanToken.Cancel();
        Console.WriteLine($"UI thread/ASP Core thread is available to perform other task. Thread Id is {Thread.CurrentThread.ManagedThreadId}");
        var sum = await task;
        Console.WriteLine($"SumOfSquare is {sum}. Thread Id is {Thread.CurrentThread.ManagedThreadId}");

    }

/// <summary>
/// State Mutation is difficult and unneccessary overhead for compiler. Here we achieved the state mutation by introducing the new variable.
/// For every loop, a object will created with value type object and that passed on to the async method.
/// 
/// </summary>
      public void StartTaskWithMutation()
  {
    var tasks = new List<Task>();
    for (var i = 1; i < 4; i++)
    {
       var iteration = i;
      var task = Task.Run(async () =>
      {
        await Task.Delay(2000);
        Console.WriteLine($"Iteration {iteration}");
      });
      Console.WriteLine($"iteration value is {iteration}");
      tasks.Add(task);
    }
    Task.WaitAll(tasks.ToArray());


  }

    public int CalculateSumOfSquare(IEnumerable<int> numbers)
    {
        int sum = 0;
        foreach (int number in numbers)
        {

            sum += number * number;
        }
        return sum;

    }
}