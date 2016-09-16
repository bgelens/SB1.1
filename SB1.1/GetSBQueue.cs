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

        protected override void ProcessRecord()
        {
            if (SBConnection.Instance.ConnectionString == null)
            {
                Console.WriteLine("Run Connect-SB first!");
            }
            else
            {
                if (ParameterSetName == "Named")
                {
                    WriteObject(SBConnection.Instance.NamespaceManager.GetQueue(Name));
                }
                else
                {
                    foreach (var queue in SBConnection.Instance.NamespaceManager.GetQueues())
                    {
                        WriteObject(queue);
                    }
                }
            }
        }
    }
}
