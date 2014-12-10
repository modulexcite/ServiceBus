//-------------------------------------------------------------------------------
// <copyright file="DeadLetterMessagesWhenRetryCountIsReachedStep.cs" company="MMS AG">
//   Copyright (c) MMS AG, 2008-2015
// </copyright>
//-------------------------------------------------------------------------------

namespace MMS.ServiceBus.Pipeline.Incoming
{
    using System;
    using System.Runtime.ExceptionServices;
    using System.Threading.Tasks;

    public class DeadLetterMessagesWhenRetryCountIsReachedStep : IIncomingLogicalStep
    {
        public async Task Invoke(IncomingLogicalContext context, IBusForHandler bus, Func<Task> next)
        {
            ExceptionDispatchInfo serializationException = null;
            try
            {
                await next()
                    .ConfigureAwait(false);
            }
            catch (Exception exception)
            {
                if (IsRetryCountReached(context))
                {
                    // We can't do async in a catch block, therefore we have to capture the exception!
                    serializationException = ExceptionDispatchInfo.Capture(exception);
                }
                else
                {
                    throw;
                }
            }

            var message = context.TransportMessage;

            // ReSharper disable PossibleNullReferenceException
            message.SetFailureHeaders(serializationException.SourceException, "Max number of retries has been reached!");
            // ReSharper restore PossibleNullReferenceException
            await message.DeadLetterAsync()
                .ConfigureAwait(false);

            // Because we instructed the message to deadletter it is safe to rethrow. The broker will not redeliver.
            serializationException.Throw();
        }

        private static bool IsRetryCountReached(IncomingLogicalContext context)
        {
            const int HardcodedMaxRetryOfServiceBusLibrary = 10;
            return context.TransportMessage.DeliveryCount > HardcodedMaxRetryOfServiceBusLibrary - 1;
        }
    }
}