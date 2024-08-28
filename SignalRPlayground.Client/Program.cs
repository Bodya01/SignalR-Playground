using SignalRPlayground.Client;

internal class Program
{
    private async static Task Main(string[] args)
    {
        await Task.Delay(5000);
        await new Application().StartAsync();
    }
}