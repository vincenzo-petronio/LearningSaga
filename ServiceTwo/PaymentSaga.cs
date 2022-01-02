using OpenSleigh.Core;
using OpenSleigh.Core.Messaging;
using SagaCommon;

namespace ServiceTwo
{
    public record PaymentSagaState : SagaState
    {
        public PaymentSagaState(Guid id) : base(id)
        {
        }
    }

    public class PaymentSaga : Saga<PaymentSagaState>, IStartedBy<BuySagaProcessWallet>
    {
        private readonly ILogger<PaymentSaga> logger;
        private readonly IWalletService walletService;

        public PaymentSaga(ILogger<PaymentSaga> logger, IWalletService walletService, PaymentSagaState state) : base(state)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.walletService = walletService ?? throw new ArgumentNullException(nameof(walletService));
        }

        public async Task HandleAsync(IMessageContext<BuySagaProcessWallet> context, CancellationToken cancellationToken = default)
        {
            logger.LogInformation($"SAGA process Payment '{context.Message.CorrelationId}'...");

            var wallet = (await walletService.GetWalletsAsync()).First();
            wallet.Amount = context.Message.Total;
            var result = await walletService.UpdateWalletAsync(wallet);

            if (result)
            {
                var msg = new BuySagaEnd(Guid.NewGuid(), context.Message.CorrelationId);
                this.Publish(msg);
            }
        }
    }
}
