using LxUtilities.Contracts.ServiceBus;

namespace LxUtilities.Compact.Tests.ServiceBus._ObjectMothers
{
    public class BusHostConfig : IBusHostConfig
    {
        public BusHostConfig(string uri, string username, string password)
        {
            Uri = uri;
            Username = username;
            Password = password;
        }

        public string Uri { get; }
        public string Username { get; }
        public string Password { get; }
    }

    public static class BusHostConfigMother
    {
        public static IBusHostConfig Default()
        {
            return new BusHostConfig("rabbitmq://localhost", "guest", "guest");
        }
    }
}
