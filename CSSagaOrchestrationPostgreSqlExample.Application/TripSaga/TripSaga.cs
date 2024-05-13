using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CrystalSharp;
using CrystalSharp.Application;
using CrystalSharp.Common.Extensions;
using CrystalSharp.Sagas;
using CSSagaOrchestrationPostgreSqlExample.Application.TripSaga.Activities;

namespace CSSagaOrchestrationPostgreSqlExample.Application.TripSaga
{
    public class TripSaga : SagaOrchestrator<TripSagaLocator, PlanTripTransaction>
    {
        public TripSaga(IResolver resolver,
            ISagaStore sagaStore,
            TripSagaLocator sagaLocator)
            : base(resolver, sagaStore, sagaLocator)
        {
            //
        }

        public override async Task<SagaTransactionResult> Handle(PlanTripTransaction request, CancellationToken cancellationToken = default)
        {
            SagaResult sagaResult = await PrepareOrchestrator(request)
                .Activity<CreateTripActivity>("Create Trip")
                .Activity<BookHotelActivity>("Book Hotel")
                .WithCompensation<CancelHotelReservationActivity>("Cancel Hotel Reservation")
                .Activity<BookFlightActivity>("Book Flight")
                .WithCompensation<CancelFlightActivity>("Cancel Flight")
                .Activity<ConfirmTripActivity>("Confirm Trip")
                .WithCompensation<CancelTripActivity>("Cancel Trip")
                .Run(cancellationToken)
                .ConfigureAwait(false);

            IEnumerable<Error> errors = sagaResult.Trail.Where(t => t.Errors.HasAny()).SelectMany(e => e.Errors);

            return new SagaTransactionResult(sagaResult.CorrelationId, sagaResult.Success, errors);
        }
    }
}
