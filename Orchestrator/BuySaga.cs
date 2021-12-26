using Microsoft.Extensions.Logging;
using OpenSleigh.Core;
using OpenSleigh.Core.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orchestrator
{

    public class BuySagaState : SagaState
    {
        public BuySagaState(Guid id) : base(id)
        {
        }
    }

    // MESSAGES
    public record BuySagaStart(Guid id, Guid correlationId) : ICommand
    {
        public Guid Id => this.Id;
        public Guid CorrelationId => this.CorrelationId;
    }
    public record BuySagaProcessProduct(Guid id, Guid correlationId) : ICommand
    {
        public Guid Id => this.Id;
        public Guid CorrelationId => this.CorrelationId;
    }
    public record BuySagaProcessWallet(Guid id, Guid correlationId) : ICommand
    {
        public Guid Id => this.Id;
        public Guid CorrelationId => this.CorrelationId;
    }
    //EVENTS
    public record BuySagaEnd(Guid id, Guid correlationId) : IEvent
    {
        public Guid Id => this.Id;
        public Guid CorrelationId => this.CorrelationId;
    }

    public class BuySaga : Saga<BuySagaState>,
        IStartedBy<BuySagaStart>,
        IHandleMessage<BuySagaProcessProduct>,
        IHandleMessage<BuySagaProcessWallet>,
        IHandleMessage<BuySagaEnd>
    {
        private readonly ILogger<BuySaga> _logger;

        public BuySaga(ILogger<BuySaga> logger, BuySagaState state) : base(state)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task HandleAsync(IMessageContext<BuySagaStart> context, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation($"SAGA start '{context.Message.CorrelationId}'...");
            throw new NotImplementedException();
        }

        public Task HandleAsync(IMessageContext<BuySagaProcessProduct> context, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation($"SAGA process Product '{context.Message.CorrelationId}'...");
            throw new NotImplementedException();
        }

        public Task HandleAsync(IMessageContext<BuySagaProcessWallet> context, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation($"SAGA process Wallet '{context.Message.CorrelationId}'...");
            throw new NotImplementedException();
        }

        public Task HandleAsync(IMessageContext<BuySagaEnd> context, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation($"SAGA end '{context.Message.CorrelationId}'...");
            throw new NotImplementedException();
        }
    }
}
