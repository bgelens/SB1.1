﻿using System;
using System.Management.Automation;
using Microsoft.ServiceBus.Messaging;

namespace ServiceBus
{
    [Cmdlet(VerbsCommon.Select, "SBQueue")]
    [OutputType(typeof(void))]
    public class SelectSBQueue : PSCmdlet
    {
        [Parameter(Mandatory = true,
            ValueFromPipeline = true,
            Position = 0)]
        [ValidateNotNull()]
        public QueueDescription Queue;

        [Parameter(Position = 1)]
        public ReceiveMode Mode = ReceiveMode.ReceiveAndDelete;
        protected override void ProcessRecord()
        {
            try
            {
                SBConnection.Instance.QueueClient = SBConnection.Instance.MessagingFactory.CreateQueueClient(Queue.Path, Mode);
            }
            catch (Exception ex)
            {
                SBConnection.Instance.QueueClient = null;
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
