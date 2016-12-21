using System;
using System.Management.Automation;
using Microsoft.ServiceBus.Messaging;

namespace ServiceBus
{
    [OutputType(typeof(BrokeredMessage))]
    [Cmdlet(VerbsCommunications.Receive,"SBTopicSubscriptionMessage")]
    public class ReceiveSBTopicSubscriptionMessage : PSCmdlet
    {
        [Parameter()]
        public TimeSpan TimeOut;

        protected override void ProcessRecord()
        {
            try
            {
                if (SBConnection.Instance.SubscriptionClient == null)
                {
                    throw new Exception("SubscriptionClient is not set. Run Select-SBTopicSubscription first!");
                }

                BrokeredMessage msg;
                if (null != TimeOut)
                {
                    msg = SBConnection.Instance.SubscriptionClient.Receive(TimeOut);
                }
                else
                {
                    msg = SBConnection.Instance.SubscriptionClient.Receive();
                }
                if (msg == null)
                {
                    return;
                }
                var messageBodyType = Type.GetType(msg.Properties["messageType"].ToString());

                if (messageBodyType == null)
                {
                    msg.DeadLetter();
                    throw new Exception(string.Format("Message does not have a messagebodytype specified, message {0}", msg.MessageId));
                }

                var method = typeof(BrokeredMessage).GetMethod("GetBody", new Type[] { });
                var generic = method.MakeGenericMethod(messageBodyType);
                /*
                if (SBConnection.Instance.SubscriptionClient.Mode == ReceiveMode.PeekLock)
                {
                    // no peeklock logic implemented yet
                    msg.Complete();
                }
                */
                var messageBody = generic.Invoke(msg, null);
                msg.Properties["Body"] = messageBody;
                WriteObject(msg);
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
