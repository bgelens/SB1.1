using System;
using System.Management.Automation;
using Microsoft.ServiceBus.Messaging;

namespace ServiceBus
{
    [Cmdlet(VerbsCommon.Remove,"SBTopic",
        ConfirmImpact = ConfirmImpact.High,
            SupportsShouldProcess = true
        )]
    public class RemoveSBTopic : PSCmdlet
    {
        [Parameter(Mandatory = true,
            ValueFromPipeline = true)]
        public TopicDescription Topic { get; set; }

        protected override void ProcessRecord()
        {
            if (ShouldProcess(Topic.Path))
            {
                try
                {
                    SBConnection.Instance.NamespaceManager.DeleteTopic(Topic.Path);
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
