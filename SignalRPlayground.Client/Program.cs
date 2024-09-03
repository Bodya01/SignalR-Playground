using SignalRPlayground.Client;

internal class Program
{
    private async static Task Main(string[] args)
    {
        await Task.Delay(5000); // for multiple startup
        await new Application().StartAsync();
    }
}