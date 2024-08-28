using SignalRPlayground.Client;

internal class Program
{
    private async static Task Main(string[] args)
    {
        Thread.Sleep(5000);
        await new Application().StartAsync();
    }
}