using OpenSleigh.Core.Messaging;

namespace SagaCommon
{
    public record BuySagaEnd(Guid Id, Guid CorrelationId) : IEvent { }
}
