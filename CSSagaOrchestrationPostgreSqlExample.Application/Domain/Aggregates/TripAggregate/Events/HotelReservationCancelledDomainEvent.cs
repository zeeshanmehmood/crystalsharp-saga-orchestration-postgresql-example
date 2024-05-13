using System;
using Newtonsoft.Json;
using CrystalSharp.Domain.Infrastructure;

namespace CSSagaOrchestrationPostgreSqlExample.Application.Domain.Aggregates.TripAggregate.Events
{
    public class HotelReservationCancelledDomainEvent : DomainEvent
    {
        public decimal HotelReservationPaidByCustomer { get; private set; }
        public bool HotelReservationConfirmed { get; private set; }

        public HotelReservationCancelledDomainEvent(Guid streamId, decimal hotelReservationPaidByCustomer, bool hotelReservationConfirmed)
        {
            StreamId = streamId;
            HotelReservationPaidByCustomer = hotelReservationPaidByCustomer;
            HotelReservationConfirmed = hotelReservationConfirmed;
        }

        [JsonConstructor]
        public HotelReservationCancelledDomainEvent(Guid streamId,
            decimal hotelReservationPaidByCustomer,
            bool hotelReservationConfirmed,
            int entityStatus,
            DateTime createdOn,
            DateTime? modifiedOn,
            long version)
        {
            StreamId = streamId;
            HotelReservationPaidByCustomer = hotelReservationPaidByCustomer;
            HotelReservationConfirmed = hotelReservationConfirmed;
            EntityStatus = entityStatus;
            CreatedOn = createdOn;
            ModifiedOn = modifiedOn;
            Version = version;
        }
    }
}
