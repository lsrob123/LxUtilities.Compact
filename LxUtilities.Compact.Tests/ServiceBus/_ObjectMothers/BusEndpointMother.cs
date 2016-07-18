using LxUtilities.Compact.ServiceBus.MassTransit;
using MassTransit;

namespace LxUtilities.Compact.Tests.ServiceBus._ObjectMothers
{
    internal static class BusEndpointMother
    {
        public static BusEndpoint WithConsumer(string endpointName, IConsumer commandBusConsumer)
        {
            return new BusEndpoint(endpointName, commandBusConsumer);
        }
    }
}
