using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CrystalSharp.Sagas;
using CSSagaOrchestrationPostgreSqlExample.Api.Dto;
using CSSagaOrchestrationPostgreSqlExample.Application.TripSaga;

namespace CSSagaOrchestrationPostgreSqlExample.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripController : ControllerBase
    {
        private readonly ISagaTransactionExecutor _sagaTransactionExecutor;

        public TripController(ISagaTransactionExecutor sagaTransactionExecutor)
        {
            _sagaTransactionExecutor = sagaTransactionExecutor;
        }

        [HttpPost]
        [Route("plan-trip")]
        public async Task<ActionResult<SagaTransactionResult>> PostPlanTrip([FromBody] PlanTripRequest request)
        {
            PlanTripTransaction transaction = new()
            {
                Name = request.Name,
                Hotel = request.Hotel,
                HotelReservationAmount = request.HotelReservationAmount,
                HotelReservationPaidByCustomer = request.HotelReservationPaidByCustomer,
                Flight = request.Flight,
                Fare = request.Fare,
                FlightFarePaidByCustomer = request.FlightFarePaidByCustomer
            };

            return await _sagaTransactionExecutor.Execute(transaction, CancellationToken.None);
        }
    }
}
