using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;

namespace ServiceBus
{
    [Cmdlet(VerbsCommunications.Connect,"SB")]
    [OutputType(typeof(SBConnection))]
    public class ConnectSB : PSCmdlet
    {
        [Parameter(Mandatory = true,
            ValueFromPipeline = true)]
        [ValidateNotNullOrEmpty()]
        public string ConnectionString { get; set; }

        protected override void ProcessRecord()
        {
            try
            {
                SBConnection.Instance.NamespaceManager = NamespaceManager.CreateFromConnectionString(ConnectionString);
                SBConnection.Instance.ConnectionString = ConnectionString;
                SBConnection.Instance.MessagingFactory = MessagingFactory.CreateFromConnectionString(ConnectionString);
                WriteObject(SBConnection.Instance);
            }
            catch (Exception)
            {
                SBConnection.Instance.NamespaceManager = null;
                SBConnection.Instance.ConnectionString = null;
                SBConnection.Instance.MessagingFactory = null;
                //throw;
            }
        }
    }
}
