//-------------------------------------------------------------------------------
// <copyright file="SyncAsAsyncHandlerDecorator.cs" company="MMS AG">
//   Copyright (c) MMS AG, 2008-2015
// </copyright>
//-------------------------------------------------------------------------------

namespace MMS.ServiceBus.Pipeline
{
    using System.Threading.Tasks;

    public class SyncAsAsyncHandlerDecorator<TMessage> : IHandleMessageAsync<TMessage>
    {
        private readonly IHandleMessage<TMessage> handler;

        public SyncAsAsyncHandlerDecorator(IHandleMessage<TMessage> handler)
        {
            this.handler = handler;
        }

        public async Task Handle(TMessage message, IBus bus)
        {
            await Task.Run(() => this.handler.Handle(message, bus)).ConfigureAwait(false);
        }
    }
}