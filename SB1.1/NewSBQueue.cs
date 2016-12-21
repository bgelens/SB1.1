using System;
using System.Management.Automation;

namespace ServiceBus
{
    [Cmdlet(VerbsCommon.New,"SBQueue")]
    public class NewSBQueue : PSCmdlet
    {
        [Parameter(Mandatory = true)]
        public string Name { get; set; }

        protected override void ProcessRecord()
        {
            try
            {
                SBConnection.Instance.NamespaceManager.CreateQueue(Name);
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
