using System;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;

namespace ServiceBus
{
    internal sealed class SBConnection
    {
        private static volatile SBConnection instance;
        private static object lockObject = new Object();
        public NamespaceManager NamespaceManager { get; set; }
        public string ConnectionString { get; set; }
        public MessagingFactory MessagingFactory { get; set; }
        public QueueClient QueueClient { get; set; }
        public TopicClient TopicClient { get; set; }
        public SubscriptionClient SubscriptionClient { get; set; }
        private SBConnection() { }

        public static SBConnection Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (lockObject)
                    {
                        if (instance == null)
                        {
                            instance = new SBConnection();
                        }
                    }
                }
                return instance;
            }
        }
    }
}
