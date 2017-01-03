using System;
using System.Management.Automation;
using Microsoft.ServiceBus.Messaging;

namespace ServiceBus
{
    [Cmdlet(VerbsCommunications.Send,"SBTopicMessage")]
    public class SendSBTopicMessage : PSCmdlet
    {
        [Parameter(Mandatory = true,
            Position = 0,
                ValueFromPipeline = true)]
        public Object InputObject;

        protected override void ProcessRecord()
        {
            try
            {
                if (SBConnection.Instance.TopicClient == null)
                {
                    throw new Exception("TopicClient is not set. Run Select-SBTopic first!");
                }
                BrokeredMessage msg = new BrokeredMessage(InputObject);
                msg.Properties["messageType"] = InputObject.GetType().AssemblyQualifiedName;
                SBConnection.Instance.TopicClient.Send(msg);
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
