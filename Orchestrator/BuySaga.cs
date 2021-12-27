using Common;
using Microsoft.Extensions.Logging;
using OpenSleigh.Core;
using OpenSleigh.Core.Messaging;

namespace Orchestrator
{

    public class BuySagaState : SagaState
    {
        public BuySagaState(Guid id) : base(id)
        {
        }

        public int Items { get; set; }
    }

    public class BuySaga : Saga<BuySagaState>,
        IStartedBy<BuySagaStart>,
        IHandleMessage<BuySagaProcessProduct>,
        IHandleMessage<BuySagaProcessWallet>,
        IHandleMessage<BuySagaEnd>
    {
        private readonly ILogger<BuySaga> _logger;
        private readonly IServices _services;

        public BuySaga(ILogger<BuySaga> logger, IServices services, BuySagaState state) : base(state)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _services = services ?? throw new ArgumentNullException(nameof(_services));
        }

        public Task HandleAsync(IMessageContext<BuySagaStart> context, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation($"SAGA start '{context.Message.CorrelationId}'...");

            this.State.Items = 100;

            var msg = new BuySagaProcessProduct(Guid.NewGuid(), context.Message.CorrelationId);
            this.Publish(msg);

            return Task.CompletedTask;
        }

        public async Task HandleAsync(IMessageContext<BuySagaProcessProduct> context, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation($"SAGA process Product '{context.Message.CorrelationId}'...");

            var products = await _services.GetAllProducts();
            var phones = products.Where(p => p.Name.Equals("phone")).FirstOrDefault();

            if (phones?.Quantity > this.State.Items)
            {
                bool result = await _services.OrderProduct(phones.Name, this.State.Items);

                if (result)
                {
                    var msg = new BuySagaProcessWallet(Guid.NewGuid(), context.Message.CorrelationId);
                    this.Publish(msg);
                }
            }
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
