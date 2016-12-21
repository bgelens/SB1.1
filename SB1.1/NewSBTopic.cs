using System;
using System.Management.Automation;

namespace ServiceBus
{
    [Cmdlet(VerbsCommon.New, "SBTopic")]
    public class NewSBTopic : PSCmdlet
    {
        [Parameter(Mandatory = true)]
        [Alias("Path")]
        public string Name { get; set; }

        protected override void ProcessRecord()
        {
            try
            {
                SBConnection.Instance.NamespaceManager.CreateTopic(Name);
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
