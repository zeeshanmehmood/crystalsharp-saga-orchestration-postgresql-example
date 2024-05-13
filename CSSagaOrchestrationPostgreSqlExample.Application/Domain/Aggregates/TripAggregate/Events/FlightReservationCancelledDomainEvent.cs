using System;
using Newtonsoft.Json;
using CrystalSharp.Domain.Infrastructure;

namespace CSSagaOrchestrationPostgreSqlExample.Application.Domain.Aggregates.TripAggregate.Events
{
    public class FlightReservationCancelledDomainEvent : DomainEvent
    {
        public decimal FlightFarePaidByCustomer { get; private set; }
        public bool FlightConfirmed { get; private set; }

        public FlightReservationCancelledDomainEvent(Guid streamId, decimal flightFarePaidByCustomer, bool flightConfirmed)
        {
            StreamId = streamId;
            FlightFarePaidByCustomer = flightFarePaidByCustomer;
            FlightConfirmed = flightConfirmed;
        }

        [JsonConstructor]
        public FlightReservationCancelledDomainEvent(Guid streamId,
            decimal flightFarePaidByCustomer,
            bool flightConfirmed,
            int entityStatus,
            DateTime createdOn,
            DateTime? modifiedOn,
            long version)
        {
            StreamId = streamId;
            FlightFarePaidByCustomer = flightFarePaidByCustomer;
            FlightConfirmed = flightConfirmed;
            EntityStatus = entityStatus;
            CreatedOn = createdOn;
            ModifiedOn = modifiedOn;
            Version = version;
        }
    }
}
