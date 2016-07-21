using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using LxUtilities.Contracts.ServiceBus;
using MassTransit;

namespace LxUtilities.Compact.ServiceBus.MassTransit
{
    public class SingleBusControl : ISingleBusControl<MassTransitBus>
    {
        protected readonly ReaderWriterLockSlim BusControlLock =
            new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);

        protected IList<BusEndpoint> ReceiveEndpoints;

        public SingleBusControl(IBusHostConfig config, IEnumerable<BusEndpoint> endpoints)
        {
            Initialize(config, endpoints);
        }

        public SingleBusControl(IBusHostConfig config, params BusEndpoint[] endpoints)
        {
            Initialize(config, endpoints);
        }

        public IBusHostConfig Config { get; private set; }
        public MassTransitBus Instance { get; private set; }

        public void Start()
        {
            BusControlLock.EnterWriteLock();
            try
            {
                var busControl = Instance as IBusControl;
                busControl?.Start();
            }
            finally
            {
                BusControlLock.ExitWriteLock();
            }
        }

        public void Stop()
        {
            BusControlLock.EnterWriteLock();
            try
            {
                var busControl = Instance as IBusControl;
                busControl?.Stop();
            }
            finally
            {
                BusControlLock.ExitWriteLock();
            }
        }

        private void Initialize(IBusHostConfig config, IEnumerable<BusEndpoint> endpoints)
        {
            Config = config;
            ReceiveEndpoints = endpoints?.ToList() ?? new List<BusEndpoint>();

            Create();
        }

        protected void Create()
        {
            BusControlLock.EnterUpgradeableReadLock();
            try
            {
                if (Instance != null)
                    return;

                BusControlLock.EnterWriteLock();
                try
                {
                    Instance = Bus.Factory.CreateUsingRabbitMq(rabbitMqBusFactoryConfigurator =>
                    {
                        var host = rabbitMqBusFactoryConfigurator.Host(new Uri(Config.Uri), rabbitMqHostConfigurator =>
                        {
                            rabbitMqHostConfigurator.Username(Config.Username);
                            rabbitMqHostConfigurator.Password(Config.Password);
                        });

                        foreach (var endpoint in ReceiveEndpoints)
                        {
                            rabbitMqBusFactoryConfigurator.ReceiveEndpoint(host, endpoint.EndpointName,
                                receiveEndpointConfigurator =>
                                {
                                    foreach (var consumer in endpoint.Consumers)
                                    {
                                        receiveEndpointConfigurator.Consumer(consumer.GetType(),
                                            consumerType => consumer);
                                    }
                                });
                        }
                    }) as MassTransitBus;

                    var busControl = (IBusControl) Instance;
                    if (busControl == null)
                        throw new NullReferenceException("Failed to create BusControl");
                }
                finally
                {
                    BusControlLock.ExitWriteLock();
                }
            }
            finally
            {
                BusControlLock.ExitUpgradeableReadLock();
            }
        }

    }
}