using System;
using System.Threading.Tasks;
using LxUtilities.Contracts.ServiceBus;
using MassTransit;
using MassTransit.Util;

namespace LxUtilities.Compact.ServiceBus.MassTransit
{
    public static class MassTransitBusExtension
    {
        public static async Task Send(this MassTransitBus bus, string hostUri, string endpointName, IBusCommand command)
        {
            var sendEndpointUri = new Uri(hostUri).AppendToPath(endpointName);
            var sendEndpoint = await (bus as ISendEndpointProvider).GetSendEndpoint(sendEndpointUri);
            await sendEndpoint.Send(command);
        }
    }
}