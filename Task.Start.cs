

internal class TaskStart
{

  /// <summary>
  /// Task start() was introduced with dotnet franework 2.0. This was the first not-full async programming feature in dotnet.
  /// Task start neither return any value nor awaited. Task start will use the current Task Scheduler to execute the action. Which 
  /// may introduce performance implication when the requested task is blocked. 
  /// Task Start can take cancallationToken and TaskCreationOption for more advanced actions.
  /// Also can pass object to its action.
  /// </summary>
  public void StartTask()
  {
    Task task = new Task(() =>
    {
      Console.WriteLine($"Inside task start and thread id is {Thread.CurrentThread.ManagedThreadId}");
      Thread.Sleep(2000);
    });
    task.Start();
    Console.WriteLine($"End of Start method and thread id is {Thread.CurrentThread.ManagedThreadId}");
  }

  /// <summary>
  /// Task Start with Object.
  /// We are passing the object into action delegates. 
  /// </summary>
  public void StartTaskWithObject()
  {
    object Name = "Unmutated";
    Task task = new Task((Name) =>
    {

      Console.WriteLine($"Inside task start and thread id is {Thread.CurrentThread.ManagedThreadId}");
      Thread.Sleep(2000);
      Console.WriteLine($"Object passed into the action is {Name}");
    }, Name);
    task.Start();

    Name = "Mutated";
    Console.WriteLine($"Object is {Name} and thread id is {Thread.CurrentThread.ManagedThreadId}");
  }


}