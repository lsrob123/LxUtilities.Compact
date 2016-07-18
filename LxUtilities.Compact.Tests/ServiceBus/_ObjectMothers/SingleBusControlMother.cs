using LxUtilities.Compact.ServiceBus.MassTransit;
using LxUtilities.Contracts.ServiceBus;
using MassTransit;

namespace LxUtilities.Compact.Tests.ServiceBus._ObjectMothers
{
    internal static class SingleBusControlMother
    {
        public static ISingleBusControl<MassTransitBus> WithDefault(BusEndpoint endpoint)
        {
            var singleBusControl = new SingleBusControl(BusHostConfigMother.Default(), endpoint);
            return singleBusControl;
        }
    }
}
