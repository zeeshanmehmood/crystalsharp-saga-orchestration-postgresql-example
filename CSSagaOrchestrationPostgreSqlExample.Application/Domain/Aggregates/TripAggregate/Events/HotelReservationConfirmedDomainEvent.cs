using System;
using Newtonsoft.Json;
using CrystalSharp.Domain.Infrastructure;

namespace CSSagaOrchestrationPostgreSqlExample.Application.Domain.Aggregates.TripAggregate.Events
{
    public class HotelReservationConfirmedDomainEvent : DomainEvent
    {
        public string Hotel { get; private set; }
        public decimal HotelReservation { get; private set; }
        public decimal HotelReservationPaidByCustomer { get; private set; }
        public bool HotelReservationConfirmed { get; private set; }

        public HotelReservationConfirmedDomainEvent(Guid streamId,
            string hotel,
            decimal hotelReservation,
            decimal hotelReservationPaidByCustomer,
            bool hotelReservationConfirmed)
        {
            StreamId = streamId;
            Hotel = hotel;
            HotelReservation = hotelReservation;
            HotelReservationPaidByCustomer = hotelReservationPaidByCustomer;
            HotelReservationConfirmed = hotelReservationConfirmed;
        }

        [JsonConstructor]
        public HotelReservationConfirmedDomainEvent(Guid streamId,
            string hotel,
            decimal hotelReservation,
            decimal hotelReservationPaidByCustomer,
            bool hotelReservationConfirmed,
            int entityStatus,
            DateTime createdOn,
            DateTime? modifiedOn,
            long version)
        {
            StreamId = streamId;
            Hotel = hotel;
            HotelReservation = hotelReservation;
            HotelReservationPaidByCustomer = hotelReservationPaidByCustomer;
            HotelReservationConfirmed = hotelReservationConfirmed;
            EntityStatus = entityStatus;
            CreatedOn = createdOn;
            ModifiedOn = modifiedOn;
            Version = version;
        }
    }
}
