using System.Threading;
using System.Threading.Tasks;
using CrystalSharp.Infrastructure.EventStoresPersistence;
using CrystalSharp.Sagas;
using CSSagaOrchestrationPostgreSqlExample.Application.Domain.Aggregates.TripAggregate;

namespace CSSagaOrchestrationPostgreSqlExample.Application.TripSaga.Activities
{
    public class CreateTripActivity : ISagaActivity
    {
        private readonly IAggregateEventStore<int> _eventStore;

        public CreateTripActivity(IAggregateEventStore<int> eventStore)
        {
            _eventStore = eventStore;
        }

        public async Task<SagaTransactionResult> Execute(SagaOrchestratorContext sagaContext, CancellationToken cancellationToken = default)
        {
            PlanTripTransaction transaction = (PlanTripTransaction)sagaContext.Data;
            Trip trip = Trip.Create(sagaContext.CorrelationId, transaction.Name);

            await _eventStore.Store(trip, cancellationToken).ConfigureAwait(false);

            return new SagaTransactionResult(sagaContext.CorrelationId, true);
        }
    }
}
