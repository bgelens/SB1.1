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
    [Cmdlet(VerbsCommon.Remove,"SBQueue"//,
        //ConfirmImpact = ConfirmImpact.High,
        //SupportsShouldProcess = true
        )]
    public class RemoveSBQueue : PSCmdlet
    {
        [Parameter(Mandatory = true,
            ValueFromPipeline = true)]
        public QueueDescription Queue { get; set; }

        protected override void ProcessRecord()
        {
            /*if (ShouldProcess.ShouldProcess == true)
            {

            }*/
            SBConnection.Instance.NamespaceManager.DeleteQueue(Queue.Path);
        }
    }
}
