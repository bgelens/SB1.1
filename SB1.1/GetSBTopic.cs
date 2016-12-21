using System;
using System.Management.Automation;
using Microsoft.ServiceBus.Messaging;

namespace ServiceBus
{
    [Cmdlet(VerbsCommon.Get, "SBTopic", DefaultParameterSetName = "List")]
    [OutputType(typeof(TopicDescription))]
    public class GetSBTopic : PSCmdlet
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
                    WriteObject(SBConnection.Instance.TopicClient);
                }
                else
                {
                    if (ParameterSetName == "Named")
                    {
                        WriteObject(SBConnection.Instance.NamespaceManager.GetTopic(Name));
                    }
                    else
                    {
                        foreach (var topic in SBConnection.Instance.NamespaceManager.GetTopics())
                        {
                            WriteObject(topic);
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
