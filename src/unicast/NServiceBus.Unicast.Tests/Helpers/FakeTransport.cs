namespace NServiceBus.Unicast.Tests.Helpers
{
    using System;
    using Transport;

    public class FakeTransport : ITransport
    {
        public void Dispose()
        {
           
        }

        public void Start(string inputqueue)
        {
        }

        public void Start(Address localAddress)
        {
        }

        public void ChangeNumberOfWorkerThreads(int targetNumberOfWorkerThreads)
        {
        }

        public void AbortHandlingCurrentMessage()
        {
           
        }

        public int NumberOfWorkerThreads
        {
            get { return 1; }
        }

        public event EventHandler<TransportMessageReceivedEventArgs> TransportMessageReceived;
        public event EventHandler<StartedMessageProcessingEventArgs> StartedMessageProcessing;
        public event EventHandler FinishedMessageProcessing;

        public event EventHandler<FailedMessageProcessingEventArgs> FailedMessageProcessing;

        public void FakeMessageBeeingProcessed(TransportMessage transportMessage)
        {
            StartedMessageProcessing(this, new StartedMessageProcessingEventArgs(transportMessage));
            TransportMessageReceived(this,new TransportMessageReceivedEventArgs(transportMessage));
            FinishedMessageProcessing(this,new EventArgs());
        }

        public void FakeMessageBeeingPassedToTheFaultManager(TransportMessage transportMessage)
        {
            try
            {
                StartedMessageProcessing(this, new StartedMessageProcessingEventArgs(transportMessage));
            }
            catch(Exception ex)
            {
                if (FailedMessageProcessing != null)
                    FailedMessageProcessing(this, new FailedMessageProcessingEventArgs(ex));
            }
            FinishedMessageProcessing(this, new EventArgs());
        }
    }
}