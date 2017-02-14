using System;
using System.Management.Automation;
using Microsoft.ServiceBus.Messaging;
using System.Security.Principal;

namespace ServiceBus
{
    [Cmdlet(VerbsCommunications.Send,"SBQueueMessage")]
    public class SendSBQueueMessage : PSCmdlet
    {
        [Parameter(Mandatory = true,
            Position = 0,
                ValueFromPipeline = true)]
        public Object InputObject;

        protected override void ProcessRecord()
        {
            try
            {
                if (SBConnection.Instance.QueueClient == null)
                {
                    throw new Exception("QueueClient is not set. Run Select-SBQueue first!");
                }
                BrokeredMessage msg = new BrokeredMessage(InputObject);
                msg.Properties["messageType"] = InputObject.GetType().AssemblyQualifiedName;
                msg.Properties["senderId"] = WindowsIdentity.GetCurrent().Name;
                SBConnection.Instance.QueueClient.Send(msg);
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
