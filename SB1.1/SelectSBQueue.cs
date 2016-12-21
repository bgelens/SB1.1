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
    [Cmdlet(VerbsCommon.Select, "SBQueue")]
    [OutputType(typeof(void))]
    public class SelectSBQueue : PSCmdlet
    {
        [Parameter(Mandatory = true,
            ValueFromPipeline = true,
            Position = 0)]
        [ValidateNotNull()]
        public QueueDescription Queue;

        [Parameter(Position = 1)]
        public ReceiveMode Mode = ReceiveMode.ReceiveAndDelete;
        protected override void ProcessRecord()
        {
            try
            {
                SBConnection.Instance.QueueClient = SBConnection.Instance.MessagingFactory.CreateQueueClient(Queue.Path, Mode);
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
