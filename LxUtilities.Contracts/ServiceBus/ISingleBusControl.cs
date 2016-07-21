namespace LxUtilities.Contracts.ServiceBus
{
    public interface ISingleBusControl<out TBusControl>
    {
        TBusControl Instance { get; }
        IBusHostConfig Config { get; }
        void Start();
        void Stop();
    }
}