﻿using Chatter.CQRS.Context;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chatter.MessageBrokers.Saga
{
    public class InMemorySagaPersister : ISagaPersister
    {
        private readonly ConcurrentDictionary<string, SagaContext> _sagaCache = new ConcurrentDictionary<string, SagaContext>();
        private readonly ILogger<InMemorySagaPersister> _logger;

        public InMemorySagaPersister(ILogger<InMemorySagaPersister> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task<SagaContext> GetById(string id)
        {
            var saga = _sagaCache.GetValueOrDefault(id, null);

            if (saga is null)
            {
                _logger.LogDebug($"No Saga found in in-memory persistance with id '{id}'");
            }
            else
            {
                _logger.LogDebug($"Saga retrieved from in-memory persistance with id '{id}' and status '{saga.Status}'");
            }

            return Task.FromResult(saga);
        }

        public Task Persist(SagaContext saga, ISagaMessage sagaMessage, IMessageHandlerContext context)
        {
            if (saga is null)
            {
                throw new ArgumentNullException(nameof(saga), $"A non-null saga is required for persistance.");
            }

            if (saga.Status.IsSuccess() || saga.Status.IsFailed())
            {
                //TODO: this will only ever clear the collection of the process that handles the finalsagamessage
                _sagaCache.Clear();
            }
            else
            {
                _sagaCache[saga.SagaId] = saga;
            }
            _logger.LogDebug($"Saga persisted to in-memory storage with id '{saga.SagaId}' and status '{saga.Status}'");
            return Task.CompletedTask;
        }
    }
}
