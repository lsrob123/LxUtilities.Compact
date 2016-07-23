using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using LxUtilities.Compact.ServiceBus.MassTransit;
using LxUtilities.Compact.Tests.ServiceBus._ObjectMothers;
using LxUtilities.Contracts.ServiceBus;
using MassTransit;
using MassTransit.Pipeline;

namespace LxUtilities.Compact.Tests.WinForm
{
    public partial class Form1 : Form, ISendObserver, IReceiveObserver, IConsumeObserver
    {
        private readonly string _endpointName = DateTime.Today.ToString("yyyyMMdd");
        private readonly ISingleBusControl<MassTransitBus> _singleBusControl;

        private DoSomethingCommand _command = new DoSomethingCommand(Guid.NewGuid());

        public Form1()
        {
            InitializeComponent();

            var consumer = BusConsumerMother.WithCommand(ProcessCommand, _command);
            _singleBusControl =
                SingleBusControlMother.WithDefault(BusEndpointMother.WithConsumer(_endpointName, consumer));

            _singleBusControl.Instance.ConnectSendObserver(this);
            _singleBusControl.Instance.ConnectReceiveObserver(this);

            //_singleBusControl.Instance.ConnectConsumer(() => consumer);

            _singleBusControl.Start();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            //var sendEndpointUri = new Uri(singleBusControl.Config.Uri).AppendToPath(endpointName);
            //var sendEndpoint = await singleBusControl.Instance.GetSendEndpoint(sendEndpointUri);
            //await sendEndpoint.Send(command);
            _command = new DoSomethingCommand(Guid.NewGuid());

            //await _singleBusControl.Instance.Send(_singleBusControl.Config.Uri, _endpointName, _command);

            //await _singleBusControl.Instance.Send(_singleBusControl.Config.Uri, "LxUtilities.Compact.Tests.ServiceBus._ObjectMothers:DoSomethingCommand", _command);

            await _singleBusControl.Instance.Publish(_command, context =>
            {
                var a = context.Message;
            });
        }

        private void ProcessCommand(ConsumeContext<DoSomethingCommand> context, object state)
        {
            //textBox1.Text = context.Message.SomeData.ToString();

            //Assert.IsNotNull(context.Message);
            //Assert.IsNotNull(state);
            //Assert.IsAssignableFrom<DoSomethingCommand>(state);
            //var doSomethingCommand = state as DoSomethingCommand;
            //if (doSomethingCommand == null)
            //    throw new ArgumentNullException(nameof(state));

            //Assert.AreEqual(doSomethingCommand.SomeData, context.Message.SomeData);
        }

        public async Task PreSend<T>(SendContext<T> context) where T : class
        {
            //textBox2.Text = nameof(PreSend) + DateTime.Now.ToShortTimeString();
            await Task.CompletedTask;
        }

        public async Task PostSend<T>(SendContext<T> context) where T : class
        {
            //textBox2.Text = nameof(PostSend) + DateTime.Now.ToShortTimeString();
            await Task.CompletedTask;
        }

        public async Task SendFault<T>(SendContext<T> context, Exception exception) where T : class
        {
            //textBox2.Text = nameof(SendFault) + DateTime.Now.ToShortTimeString();
            await Task.CompletedTask;
        }

        public async Task PreReceive(ReceiveContext context)
        {
            await Task.CompletedTask;
        }

        public async Task PostReceive(ReceiveContext context)
        {
            await Task.CompletedTask;
        }

        public async Task PostConsume<T>(ConsumeContext<T> context, TimeSpan duration, string consumerType) where T : class
        {
            await Task.CompletedTask;
        }

        public async Task ConsumeFault<T>(ConsumeContext<T> context, TimeSpan duration, string consumerType, Exception exception) where T : class
        {
            await Task.CompletedTask;
        }

        public async Task ReceiveFault(ReceiveContext context, Exception exception)
        {
            await Task.CompletedTask;
        }

        public async Task PreConsume<T>(ConsumeContext<T> context) where T : class
        {
            await Task.CompletedTask;
        }

        public async Task PostConsume<T>(ConsumeContext<T> context) where T : class
        {
            await Task.CompletedTask;
        }

        public async Task ConsumeFault<T>(ConsumeContext<T> context, Exception exception) where T : class
        {
            await Task.CompletedTask;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            _singleBusControl.Stop();
        }
    }
}