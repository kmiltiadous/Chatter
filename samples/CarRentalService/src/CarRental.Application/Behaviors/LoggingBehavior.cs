﻿using Chatter.CQRS;
using Chatter.CQRS.Context;
using Chatter.CQRS.Pipeline;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace CarRental.Application.Behaviors
{
    public class LoggingBehavior : ICommandBehavior
    {
        private readonly ILogger<LoggingBehavior> _logger;

        public LoggingBehavior(ILogger<LoggingBehavior> logger)
        {
            _logger = logger;
        }

        public async Task Handle<TMessage>(TMessage message, IMessageHandlerContext messageHandlerContext, CommandHandlerDelegate next) where TMessage : IMessage
        {
            _logger.LogInformation($"Executed '{this.GetType().Name}'. Pre-delegate.");
            await next();
            _logger.LogInformation($"Executed '{this.GetType().Name}'. Post-delegate.");
        }
    }
}