using System;
using System.Management.Automation;
using Microsoft.ServiceBus.Messaging;

namespace ServiceBus
{
    [Cmdlet(VerbsCommon.Remove,"SBQueue",
        ConfirmImpact = ConfirmImpact.High,
            SupportsShouldProcess = true
        )]
    public class RemoveSBQueue : PSCmdlet
    {
        [Parameter(Mandatory = true,
            ValueFromPipeline = true)]
        public QueueDescription Queue { get; set; }

        protected override void ProcessRecord()
        {
            if (ShouldProcess(Queue.Path))
            {
                try
                {
                    SBConnection.Instance.NamespaceManager.DeleteQueue(Queue.Path);
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
}
