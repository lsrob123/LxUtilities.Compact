using System.Collections.Generic;
using System.Linq;
using MassTransit;

namespace LxUtilities.Compact.ServiceBus.MassTransit
{
    public class BusEndpoint
    {
        public BusEndpoint(string endpointName, params IConsumer[] consumers)
        {
            Initialize(endpointName, consumers);
        }

        public BusEndpoint(string endpointName, IEnumerable<IConsumer> consumers)
        {
            Initialize(endpointName, consumers);
        }

        public string EndpointName { get; protected set; }
        public IList<IConsumer> Consumers { get; protected set; }

        private void Initialize(string endpointName, IEnumerable<IConsumer> consumers)
        {
            EndpointName = endpointName;
            Consumers = consumers?.ToList() ?? new List<IConsumer>();
        }
    }
}