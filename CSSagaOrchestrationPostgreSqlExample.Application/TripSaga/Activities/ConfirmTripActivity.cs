using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CrystalSharp.Application;
using CrystalSharp.Common.Settings;
using CrystalSharp.Infrastructure.EventStoresPersistence;
using CrystalSharp.Sagas;
using CSSagaOrchestrationPostgreSqlExample.Application.Domain.Aggregates.TripAggregate;

namespace CSSagaOrchestrationPostgreSqlExample.Application.TripSaga.Activities
{
    public class ConfirmTripActivity : ISagaActivity
    {
        private readonly IAggregateEventStore<int> _eventStore;

        public ConfirmTripActivity(IAggregateEventStore<int> eventStore)
        {
            _eventStore = eventStore;
        }

        public async Task<SagaTransactionResult> Execute(SagaOrchestratorContext sagaContext, CancellationToken cancellationToken = default)
        {
            Trip trip = await _eventStore.Get<Trip>(sagaContext.CorrelationId, cancellationToken).ConfigureAwait(false);

            if (trip == null)
            {
                return SagaTransactionResult.WithError(new List<Error> { new Error(ReservedErrorCode.SystemError, "Trip not found.") });
            }

            trip.ConfirmTrip();
            await _eventStore.Store(trip, cancellationToken);

            return new SagaTransactionResult(sagaContext.CorrelationId, true);
        }
    }
}
