
namespace Deployer
{
    public delegate void SendMessageToConsoleEventHandler(SendMessageToConsoleEventArgs e);
    public class SendMessageToConsoleEventArgs : System.EventArgs
    {
        public string Message { get; }
        public SendMessageToConsoleEventArgs(string message)
        {
            Message = message;
        }
    }
}
