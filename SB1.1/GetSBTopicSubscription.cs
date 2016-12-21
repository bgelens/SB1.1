using System;
using System.Management.Automation;
using Microsoft.ServiceBus.Messaging;

namespace ServiceBus
{
    [OutputType(typeof(SubscriptionDescription))]
    [Cmdlet(VerbsCommon.Get,"SBTopicSubscription")]
    public class GetSBTopicSubscription : PSCmdlet
    {
        [Parameter()]
        public string Name;

        protected override void ProcessRecord()
        {
            try
            {
                if (Name == null)
                {
                    foreach (var subscription in SBConnection.Instance.NamespaceManager.GetSubscriptions(SBConnection.Instance.TopicClient.Path))
                    {
                        WriteObject(subscription);
                    }
                }
                else
                {
                    WriteObject(SBConnection.Instance.NamespaceManager.GetSubscription(SBConnection.Instance.TopicClient.Path, Name));
                }
            }
            catch (Exception ex)
            {
                ErrorRecord er = new ErrorRecord(
                    ex,
                    "Failed",
                    ErrorCategory.ObjectNotFound,
                    this
                );
                WriteError(er);
            }
        }
    }
}
