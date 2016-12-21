using System;
using Microsoft.ServiceBus.Messaging;
using System.Management.Automation;

namespace ServiceBus
{
    [Cmdlet(VerbsCommon.New, "SBTopicSubscription")]
    public class NewSBTopicSubscription : PSCmdlet
    {
        [Parameter()]
        public string Name { get; set; }

        protected override void ProcessRecord()
        {
            try
            {
                if (SBConnection.Instance.TopicClient == null)
                {
                    throw new Exception("TopicClient is not set. Run Select-SBTopic first!");
                }
                SBConnection.Instance.NamespaceManager.CreateSubscription(SBConnection.Instance.TopicClient.Path, Name);
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
