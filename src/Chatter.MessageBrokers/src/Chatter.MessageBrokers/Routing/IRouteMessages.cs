﻿using Chatter.CQRS;
using Chatter.MessageBrokers.Context;
using Chatter.MessageBrokers.Routing.Options;
using Chatter.MessageBrokers.Sending;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chatter.MessageBrokers.Routing
{
    /// <summary>
    /// Routes a brokered message to a receiver
    /// </summary>
    public interface IRouteMessages
    {
        public Task Route<TMessage, TOptions>(TMessage message, TransactionContext transactionContext, TOptions options)
            where TMessage : IMessage
            where TOptions : RoutingOptions, new();

        public Task Route<TMessage, TOptions>(TMessage message, string destinationPath, TransactionContext transactionContext, TOptions options)
            where TMessage : IMessage
            where TOptions : RoutingOptions, new();

        /// <summary>
        /// Routes a brokered message to a receiver
        /// </summary>
        /// <param name="outboundBrokeredMessage">The outbound brokered message to be routed to a receiver</param>
        /// <param name="transactionContext">The transactional information to used while routing</param>
        /// <returns>An awaitable <see cref="Task"/></returns>
        Task Route(OutboundBrokeredMessage outboundBrokeredMessage, TransactionContext transactionContext);

        /// <summary>
        /// Routes a batch of brokered messages
        /// </summary>
        /// <param name="outboundBrokeredMessages">The batch of outbound brokered messages to be routed to their receivers</param>
        /// <param name="transactionContext">The transactional information to used while routing</param>
        /// <returns>An awaitable <see cref="Task"/></returns>
        Task Route(IList<OutboundBrokeredMessage> outboundBrokeredMessages, TransactionContext transactionContext);
    }
}
