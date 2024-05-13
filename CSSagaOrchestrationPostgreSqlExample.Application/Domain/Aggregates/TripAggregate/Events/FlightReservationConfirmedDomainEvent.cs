using System;
using Newtonsoft.Json;
using CrystalSharp.Domain.Infrastructure;

namespace CSSagaOrchestrationPostgreSqlExample.Application.Domain.Aggregates.TripAggregate.Events
{
    public class FlightReservationConfirmedDomainEvent : DomainEvent
    {
        public string Flight { get; private set; }
        public decimal FlightFare { get; private set; }
        public decimal FlightFarePaidByCustomer { get; private set; }
        public bool FlightConfirmed { get; private set; }

        public FlightReservationConfirmedDomainEvent(Guid streamId,
            string flight,
            decimal flightFare,
            decimal flightFarePaidByCustomer,
            bool flightConfirmed)
        {
            StreamId = streamId;
            Flight = flight;
            FlightFare = flightFare;
            FlightFarePaidByCustomer = flightFarePaidByCustomer;
            FlightConfirmed = flightConfirmed;
        }

        [JsonConstructor]
        public FlightReservationConfirmedDomainEvent(Guid streamId,
            string flight,
            decimal flightFare,
            decimal flightFarePaidByCustomer,
            bool flightConfirmed,
            int entityStatus,
            DateTime createdOn,
            DateTime? modifiedOn,
            long version)
        {
            StreamId = streamId;
            Flight = flight;
            FlightFare = flightFare;
            FlightFarePaidByCustomer = flightFarePaidByCustomer;
            FlightConfirmed = flightConfirmed;
            EntityStatus = entityStatus;
            CreatedOn = createdOn;
            ModifiedOn = modifiedOn;
            Version = version;
        }
    }
}
