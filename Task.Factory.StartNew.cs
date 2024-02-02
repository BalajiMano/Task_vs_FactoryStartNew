
using System.Text.Json;
using Newtonsoft.Json;

internal class Taskfactory
{
     /// <summary>
    /// Task.Factory.StartNew is the advanced version of Task.Start().
    /// Unlike Task.Start(), we can await this task and it can return a value.
    /// It is also schedule the work to curent task scheduler, which mean UI thread task scheduler have to manage these task.
    /// We can pass object to delegate methods for more advanced customziation.
    /// </summary>
    public async void Start()
    {
      
        /*
        // string weatherstatus;
        var task = await Task.Factory.StartNew<Task<IEnumerable<CatFact>>>(async () =>
        {

            var client = new HttpClient();

            var response = await client.GetAsync("https://cat-fact.herokuapp.com/facts/random?animal_type=cat&amount=5");

            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            Console.WriteLine(body);

            var jsoncon = JsonConvert.DeserializeObject<IEnumerable<CatFact>>(body);

            return jsoncon;
            //return body;

        });
        // var result= await task;
        foreach (var res in task.Result)
        {
            Console.WriteLine($"Body content is {res.text}");
        }


        Console.WriteLine($"End of Start method and thread id is {Thread.CurrentThread.ManagedThreadId}");
        //  return result;
*/

        var task = Task.Factory.StartNew(() =>
        {
            // Random random=new Random(1000);
            var numbers = Enumerable.Range(10000, 1000000).ToList();
            return CalculateSumOfSquare(numbers);
        });
         Console.WriteLine($"UI thread/ASP Core thread is available to perform other task. Thread Id is {Thread.CurrentThread.ManagedThreadId}");
         var sum= await task;
         Console.WriteLine($"SumOfSquare is {sum}. Thread Id is {Thread.CurrentThread.ManagedThreadId}");
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
/// <summary>
/// State Mutation is easy and smooth in both Task.Start and factory start by just pssing an object.
/// This object will be assigned to Task Object before queued to threadpool task scheduler.
/// </summary>
    public void StartFactoryTaskWithMutation()
  {
    var tasks = new List<Task>();
    for (var i = 1; i < 4; i++)
    {
     
      var task = Task.Factory.StartNew(async (i) =>
      {
        await Task.Delay(1000);
        Console.WriteLine($"Iteration {i}");
      },i);
      Console.WriteLine($"iteration value is {i}");
      tasks.Add(task);
    }
    Task.WaitAll(tasks.ToArray());


  }
}

public class CatFact
{
    public string text { get; set; }
    public string user { get; set; }
    public string _id { get; set; }
    public string? type { get; set; }
    public bool deleted { get; set; }
    public DateTime createdAt { get; set; }

    public DateTime updatedAt { get; set; }

    public Status? status { get; set; }

    public int __v { get; set; }
}

public class Status
{
    public string? verified { get; set; } = null;
    public int sentCount { get; set; }
}
