﻿using Chatter.CQRS.Context;
using Chatter.MessageBrokers.Receiving;

namespace Chatter.MessageBrokers.Context
{
    public static class MessageHandlerContextExtensions
    {
        /// <summary>
        /// Gets contextual information about a message broker from message handler context
        /// </summary>
        /// <param name="messageHandlerContext">The message handler context</param>
        /// <returns>The message broker context</returns>
        public static MessageBrokerContext AsMessageBrokerContext(this IMessageHandlerContext messageHandlerContext)
        {
            return messageHandlerContext as MessageBrokerContext;
        }

        /// <summary>
        /// Gets contextual information about the transaction the message broker is a part of
        /// </summary>
        /// <param name="messageHandlerContext">The message handler context</param>
        /// <returns>The transaction context</returns>
        public static TransactionContext GetTransactionContext(this IMessageHandlerContext messageHandlerContext)
        {
            return messageHandlerContext.Get<TransactionContext>();
        }

        /// <summary>
        /// Gets contextual information about the reply destination
        /// </summary>
        /// <param name="messageHandlerContext">The message handler context</param>
        /// <returns>The reply destination context</returns>
        public static ReplyDestinationContext GetReplyContext(this IMessageHandlerContext messageHandlerContext)
        {
            return messageHandlerContext.Get<ReplyDestinationContext>();
        }

        /// <summary>
        /// Gets contextual information about the next destination
        /// </summary>
        /// <param name="messageHandlerContext">The message handler context</param>
        /// <returns>The next destination context</returns>
        public static NextDestinationContext GetNextDestinationContext(this IMessageHandlerContext messageHandlerContext)
        {
            return messageHandlerContext.Get<NextDestinationContext>();
        }

        /// <summary>
        /// Gets contextual information about the compensation destination
        /// </summary>
        /// <param name="messageHandlerContext">The message handler context</param>
        /// <returns>The compensation destination context</returns>
        public static CompensateContext GetCompensationContext(this IMessageHandlerContext messageHandlerContext)
        {
            return messageHandlerContext.Get<CompensateContext>();
        }

        /// <summary>
        /// Gets the inbound brokered message from the message handler context or null if the message handler context
        /// doesn't contain any contextual information about the message broker.
        /// </summary>
        /// <param name="messageHandlerContext"></param>
        /// <returns></returns>
        public static InboundBrokeredMessage GetInboundBrokeredMessage(this IMessageHandlerContext messageHandlerContext)
        {
            if (messageHandlerContext is IMessageBrokerContext brokeredContext)
            {
                return brokeredContext.BrokeredMessage;
            }

            return default;
        }

        private static T Get<T>(this IMessageHandlerContext messageHandlerContext)
        {
            messageHandlerContext.Container.TryGet<T>(out var context);
            return context;
        }
    }
}
