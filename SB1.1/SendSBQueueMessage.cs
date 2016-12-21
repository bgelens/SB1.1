using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation;
using System.Management.Automation.Language;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;

namespace ServiceBus
{
    [Cmdlet(VerbsCommunications.Send,"SBQueueMessage")]
    public class SendSBQueueMessage : PSCmdlet
    {
        [Parameter(Mandatory = true,
            Position = 0,
                ValueFromPipeline = true)]
        public PSObject InputObject;

        protected override void ProcessRecord()
        {
            try
            {
                if (SBConnection.Instance.QueueClient == null)
                {
                    throw new Exception();
                }
                    BrokeredMessage msg = new BrokeredMessage(InputObject);
                    msg.Properties["messageType"] = InputObject.GetType().AssemblyQualifiedName;
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
