

internal class TaskStart
{

public  void Start()
{
    Task task= new Task(async ()=>
    {
        // Task.Delay(2000);
         using(HttpClient _client= new HttpClient())
         {
           var hostConnect= await _client.GetAsync("https://google.com");
                if(hostConnect.IsSuccessStatusCode)
                {
                    var content=await hostConnect.Content.ReadAsStringAsync();
                    Console.WriteLine($"Google host Content Length is {content.Length}");
                }
         Console.WriteLine($"Streaming is done by {Thread.CurrentThread.ManagedThreadId}");
         }
    });
    task.Start();
  Console.WriteLine($"End of Start method and thread id is {Thread.CurrentThread.ManagedThreadId}");
}
}