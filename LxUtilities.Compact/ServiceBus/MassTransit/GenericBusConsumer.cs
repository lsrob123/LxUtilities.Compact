using System;
using System.Threading.Tasks;
using LxUtilities.Contracts.ServiceBus;
using MassTransit;

namespace LxUtilities.Compact.ServiceBus.MassTransit
{
    public class GenericBusBusConsumer<TBusMessage> : BusConsumerBase<TBusMessage>
        where TBusMessage : class, IBusMessage
    {
        protected readonly Action<ConsumeContext<TBusMessage>, object> ConsumeAction;

        public GenericBusBusConsumer(Action<ConsumeContext<TBusMessage>, object> consumeAction, object state = null)
        {
            ConsumeAction = consumeAction;
            State = state;
        }

        public object State { get; }

        public override async Task Consume(ConsumeContext<TBusMessage> context)
        {
            if (ConsumeAction != null)
                await Task.Run(() => ConsumeAction(context, State));
        }
    }
}
