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
    [Cmdlet(VerbsCommon.Get,"SBQueue",DefaultParameterSetName = "List")]
    [OutputType(typeof(QueueDescription))]
    public class GetSBQueue : PSCmdlet
    {
        [Parameter(Mandatory = true,
            ValueFromPipeline = true,
                Position = 0,
                    ParameterSetName = "Named")]
        [ValidateNotNullOrEmpty()]
        public string Name { get; set; }

        [Parameter(Mandatory = true,
            Position = 1)]
        [ValidateNotNullOrEmpty()]
        public string ConnectionString { get; set; }

        protected override void ProcessRecord()
        {
            NamespaceManager NM = NamespaceManager.CreateFromConnectionString(ConnectionString);

            if (ParameterSetName == "Named")
            {
                WriteObject(NM.GetQueue(Name));
            }
            else
            {
                foreach (var queue in NM.GetQueues())
                {
                    WriteObject(queue);
                }
            }
        }
    }
}
