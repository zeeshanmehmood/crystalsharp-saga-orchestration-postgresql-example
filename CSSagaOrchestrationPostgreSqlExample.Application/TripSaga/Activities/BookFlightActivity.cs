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
    public class BookFlightActivity : ISagaActivity
    {
        private readonly IAggregateEventStore<int> _eventStore;

        public BookFlightActivity(IAggregateEventStore<int> eventStore)
        {
            _eventStore = eventStore;
        }

        public async Task<SagaTransactionResult> Execute(SagaOrchestratorContext sagaContext, CancellationToken cancellationToken = default)
        {
            PlanTripTransaction transaction = (PlanTripTransaction)sagaContext.Data;
            Trip trip = await _eventStore.Get<Trip>(sagaContext.CorrelationId, cancellationToken).ConfigureAwait(false);

            if (trip == null)
            {
                return SagaTransactionResult.WithError(new List<Error> { new Error(ReservedErrorCode.SystemError, "Trip not found.") });
            }

            trip.BookFlight(transaction.Flight, transaction.Fare, transaction.FlightFarePaidByCustomer);
            await _eventStore.Store(trip, cancellationToken);

            return new SagaTransactionResult(sagaContext.CorrelationId, true);
        }
    }
}
