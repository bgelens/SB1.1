using System;
using System.Management.Automation;
using Microsoft.ServiceBus.Messaging;

namespace ServiceBus
{
    [Cmdlet(VerbsCommon.Select, "SBTopicSubscription")]
    [OutputType(typeof(void))]
    public class SelectSBTopicSusbscription : PSCmdlet
    {
        [Parameter(Mandatory = true,
            ValueFromPipeline = true,
            Position = 0)]
        [ValidateNotNull()]
        public SubscriptionDescription Subscription;

        protected override void ProcessRecord()
        {
            try
            {
                SBConnection.Instance.SubscriptionClient = SBConnection.Instance.MessagingFactory.CreateSubscriptionClient(Subscription.TopicPath,Subscription.Name);
            }
            catch (Exception ex)
            {
                SBConnection.Instance.QueueClient = null;
                ErrorRecord er = new ErrorRecord(
                    ex,
                    "Failed",
                    ErrorCategory.ConnectionError,
                    this
                );
                WriteError(er);
            }
        }
    }
}
