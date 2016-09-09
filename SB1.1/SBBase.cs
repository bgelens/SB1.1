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
    public abstract class SBBase : PSCmdlet
    {
        protected NamespaceManager NMgr
        {
            get
            {
                if (NM == null)
                {
                    NM = NamespaceManager.CreateFromConnectionString(ConnectionString);
                }
                return NM;
            }
        }

        private NamespaceManager NM;

        [Parameter()]
        [ValidateNotNullOrEmpty()]
        public string ConnectionString { get; set; }
    }
}
