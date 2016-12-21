using System;
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

        protected override void ProcessRecord()
        {
            try
            {
                SBConnection.Instance.NamespaceManager = NamespaceManager.CreateFromConnectionString(ConnectionString);
                SBConnection.Instance.ConnectionString = ConnectionString;
                SBConnection.Instance.MessagingFactory = MessagingFactory.CreateFromConnectionString(ConnectionString);
                WriteObject(SBConnection.Instance);
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
