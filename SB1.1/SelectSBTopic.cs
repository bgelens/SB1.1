using System;
using System.Management.Automation;
using Microsoft.ServiceBus.Messaging;

namespace ServiceBus
{
    [Cmdlet(VerbsCommon.Select, "SBTopic")]
    [OutputType(typeof(void))]
    public class SelectSBTopic : PSCmdlet
    {
        [Parameter(Mandatory = true,
            ValueFromPipeline = true,
            Position = 0)]
        [ValidateNotNull()]
        public TopicDescription Topic;

        protected override void ProcessRecord()
        {
            try
            {
                SBConnection.Instance.TopicClient = SBConnection.Instance.MessagingFactory.CreateTopicClient(Topic.Path);
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
