using CrystalSharp.Sagas;

namespace CSSagaOrchestrationPostgreSqlExample.Application.TripSaga
{
    public class PlanTripTransaction : ISagaTransaction
    {
        public string Name { get; set; }
        public string Hotel { get; set; }
        public decimal HotelReservationAmount { get; set; }
        public decimal HotelReservationPaidByCustomer { get; set; }
        public string Flight { get; set; }
        public decimal Fare { get; set; }
        public decimal FlightFarePaidByCustomer { get; set; }
    }
}
