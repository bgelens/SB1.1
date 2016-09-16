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
    [Cmdlet(VerbsCommon.New,"SBQueue")]
    public class NewSBQueue : PSCmdlet
    {
        [Parameter(Mandatory = true)]
        public string Name { get; set; }

        protected override void ProcessRecord()
        {
            SBConnection.Instance.NamespaceManager.CreateQueue(Name);
        }
    }
}
