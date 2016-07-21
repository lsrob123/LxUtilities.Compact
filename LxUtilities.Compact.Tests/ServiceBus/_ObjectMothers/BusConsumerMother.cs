using System;
using LxUtilities.Compact.ServiceBus.MassTransit;
using LxUtilities.Contracts.ServiceBus;
using MassTransit;

namespace LxUtilities.Compact.Tests.ServiceBus._ObjectMothers
{
    public class DoSomethingCommand : IBusCommand
    {
        public DoSomethingCommand(Guid someData)
        {
            SomeData = someData;
        }

        public Guid SomeData { get; }
    }

    public class SomethingHappenedCommand : IBusEvent
    {
        public SomethingHappenedCommand(Guid someData)
        {
            SomeData = someData;
        }

        public Guid SomeData { get; }
    }

    public static class BusConsumerMother
    {
        public static GenericBusBusConsumer<DoSomethingCommand> WithCommand(
            Action<ConsumeContext<DoSomethingCommand>, object> processCommandAction, DoSomethingCommand command)
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command));
            return new GenericBusBusConsumer<DoSomethingCommand>(processCommandAction, command);
        }
    }
}