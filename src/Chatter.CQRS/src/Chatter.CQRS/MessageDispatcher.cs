﻿using Chatter.CQRS.Context;
using System;
using System.Threading.Tasks;

namespace Chatter.CQRS
{
    ///<inheritdoc/>
    internal class MessageDispatcher : IMessageDispatcher
    {
        private readonly IMessageDispatcherFactory _dispatcherFactory;

        public MessageDispatcher(IMessageDispatcherFactory dispatcherFactory)
        {
            _dispatcherFactory = dispatcherFactory ?? throw new ArgumentNullException(nameof(dispatcherFactory));
        }

        ///<inheritdoc/>
        public Task Dispatch<TMessage>(TMessage message) where TMessage : IMessage
        {
            return Dispatch(message, new MessageHandlerContext());
        }

        ///<inheritdoc/>
        public Task Dispatch<TMessage>(TMessage message, IMessageHandlerContext messageHandlerContext) where TMessage : IMessage
        {
            var dispatcher = _dispatcherFactory.CreateDispatcher<TMessage>();
            return dispatcher.Dispatch(message, messageHandlerContext);
        }
    }
}
