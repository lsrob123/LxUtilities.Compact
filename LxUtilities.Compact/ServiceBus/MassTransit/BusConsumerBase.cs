using System.Threading.Tasks;
using LxUtilities.Contracts.ServiceBus;
using MassTransit;

namespace LxUtilities.Compact.ServiceBus.MassTransit
{
    public abstract class BusConsumerBase<TBusMessage> : IConsumer<TBusMessage>
        where TBusMessage : class, IBusMessage
    {
        public abstract Task Consume(ConsumeContext<TBusMessage> context);
    }
}
