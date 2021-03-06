﻿using Chatter.CQRS;
using Chatter.CQRS.Context;
using Chatter.CQRS.Pipeline;
using Chatter.MessageBrokers.Context;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Chatter.MessageBrokers.Reliability
{
    public sealed class UnitOfWorkBehavior<TMessage> : ICommandBehavior<TMessage> where TMessage : IMessage
    {
        private readonly IUnitOfWork _unitOfWork;

        public UnitOfWorkBehavior(IUnitOfWork unitOfWork)
            => _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

        public async Task Handle(TMessage message, IMessageHandlerContext messageHandlerContext, CommandHandlerDelegate next)
        {
            var transactionContext = messageHandlerContext.Container.GetOrAdd(() => new TransactionContext());
            await _unitOfWork.ExecuteAsync(() => next(), transactionContext);
        }
    }
}
