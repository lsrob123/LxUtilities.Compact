using System;
using LxUtilities.Compact.ServiceBus.MassTransit;
using LxUtilities.Contracts.ServiceBus;
using MassTransit;
using NUnit.Framework;

namespace LxUtilities.Compact.Tests.ServiceBus._ObjectMothers
{
    internal class DoSomethingCommand : IBusCommand
    {
        public DoSomethingCommand(Guid someData)
        {
            SomeData = someData;
        }

        public Guid SomeData { get; }
    }

    internal class SomethingHappenedCommand : IBusEvent
    {
        public SomethingHappenedCommand(Guid someData)
        {
            SomeData = someData;
        }

        public Guid SomeData { get; }
    }

    internal static class BusConsumerMother
    {
        public static GenericBusBusConsumer<DoSomethingCommand> WithCommand(DoSomethingCommand command,
            Action<ConsumeContext<DoSomethingCommand>, object> processCommandAction)
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command));
            return new GenericBusBusConsumer<DoSomethingCommand>(processCommandAction, command);
        }

    }
}