using System;
using LxUtilities.Compact.ServiceBus.MassTransit;
using LxUtilities.Compact.Tests.ServiceBus._ObjectMothers;
using MassTransit;
using NUnit.Framework;

namespace LxUtilities.Compact.Tests.ServiceBus.MassTransit
{
    [TestFixture]
    public class ServiceBusTests
    {
        [Test]
        public async void Given_BusCommand_When_SendIsCalled_Then_CommandConsumerConsumesTheCommand()
        {
            var endpointName = DateTime.Today.ToString("yyyyMMdd");
            var command=new DoSomethingCommand(Guid.NewGuid());
            var consumer = BusConsumerMother.WithCommand(ProcessCommand, command);
            var singleBusControl =
                SingleBusControlMother.WithDefault(BusEndpointMother.WithConsumer(endpointName, consumer));

            singleBusControl.Start();

            //var sendEndpointUri = new Uri(singleBusControl.Config.Uri).AppendToPath(endpointName);
            //var sendEndpoint = await singleBusControl.Instance.GetSendEndpoint(sendEndpointUri);
            //await sendEndpoint.Send(command);

            await singleBusControl.Instance.Send(singleBusControl.Config.Uri, endpointName, command);

            //singleBusControl.Stop();
        }

        private static void ProcessCommand(ConsumeContext<DoSomethingCommand> context, object state)
        {
            Assert.IsNotNull(context.Message);
            Assert.IsNotNull(state);
            Assert.IsAssignableFrom<DoSomethingCommand>(state);
            var doSomethingCommand = state as DoSomethingCommand;
            if (doSomethingCommand == null)
                throw new ArgumentNullException(nameof(state));

            Assert.AreEqual(doSomethingCommand.SomeData, context.Message.SomeData);
        }

    }
}
