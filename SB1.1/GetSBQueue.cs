using System;
using System.Management.Automation;
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
        [Alias("Path")]
        public string Name { get; set; }

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
                if (SBConnection.Instance.ConnectionString == null)
                {
                    throw new Exception("Run Connect-SB first!");
                }
                if (current)
                {
                    WriteObject(SBConnection.Instance.QueueClient);
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
