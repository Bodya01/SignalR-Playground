namespace SignalRPlayground.Client
{
    internal sealed class Application
    {
        private readonly SignalRClient _signalRClient;

        public Application()
        {
            _signalRClient = new SignalRClient(@"https://localhost:7244/playground-hub");
        }

        public async Task StartAsync()
        {
            Console.WriteLine("Application started!");

            await _signalRClient.StartAsync();
            await _signalRClient.JoinGroupAsync("console-client-group");

            Console.ReadLine();
            Console.WriteLine("Application shutting down!");
        }
    }
}