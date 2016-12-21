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
    [Cmdlet(VerbsCommunications.Connect, "SB")]
    [OutputType(typeof(SBConnection))]
    public class ConnectSB : PSCmdlet
    {
        [Parameter(Mandatory = true,
            ValueFromPipeline = true,
            ParameterSetName = "Connect")]
        [ValidateNotNullOrEmpty()]
        public string ConnectionString { get; set; }

        [Parameter(ParameterSetName = "Current")]
        public SwitchParameter Current
        {
            get { return current; }
            set { current = value; }
        }
        private bool current;
        protected override void ProcessRecord()
        {
            try
            {
                if (current)
                {
                    WriteObject(SBConnection.Instance);
                }
                else
                {
                    SBConnection.Instance.NamespaceManager = NamespaceManager.CreateFromConnectionString(ConnectionString);
                    SBConnection.Instance.ConnectionString = ConnectionString;
                    SBConnection.Instance.MessagingFactory = MessagingFactory.CreateFromConnectionString(ConnectionString);
                    WriteObject(SBConnection.Instance);
                }
            }
            catch (Exception ex)
            {
                SBConnection.Instance.NamespaceManager = null;
                SBConnection.Instance.ConnectionString = null;
                SBConnection.Instance.MessagingFactory = null;
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
