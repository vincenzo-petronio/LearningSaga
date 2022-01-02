using OpenSleigh.Core.Messaging;

namespace SagaCommon
{
    public record BuySagaStart(Guid Id, Guid CorrelationId) : ICommand { }

    public record BuySagaProcessProduct(Guid Id, Guid CorrelationId) : ICommand { }

    public record BuySagaProcessWallet(Guid Id, Guid CorrelationId) : ICommand
    {
        public long Total { get; set; }
    }
}
