using System.Collections.Concurrent;
{
    for (int i = 0; i < 1000; i++)
    {
        var CapturedeValue = i;
        ThreadPool.QueueUserWorkItem(delegate
        {
            Console.WriteLine(CapturedeValue);
            Thread.Sleep(10000);
        });
    }
}
Console.ReadLine();
static class MyThreadPool
{
    private static readonly BlockingCollection<Action> s_WorkItem = new();
    public static void QueueUserWorkItem(Action action) => s_WorkItem.Add(action);
    static MyThreadPool()
    {
        for (var i = 0; i < Environment.ProcessorCount; i++)
        {
            new Thread(() =>
            {
                while (true)
                {
                    new Thread(() =>
                    {
                        Action WorkItem = s_WorkItem.Take();
                        WorkItem();

                    })
                    { IsBackground = true }.Start();
                }
            });
            }
    }
}
            
            
         
            
            
    
