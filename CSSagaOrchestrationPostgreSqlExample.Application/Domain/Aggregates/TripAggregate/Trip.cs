using System;
using CrystalSharp.Domain;
using CSSagaOrchestrationPostgreSqlExample.Application.Domain.Aggregates.TripAggregate.Events;

namespace CSSagaOrchestrationPostgreSqlExample.Application.Domain.Aggregates.TripAggregate
{
    public class Trip : AggregateRoot<int>
    {
        public Guid CorrelationId { get; private set; }
        public string Name { get; private set; }
        public string Hotel { get; private set; }
        public decimal HotelReservation { get; private set; }
        public decimal HotelReservationPaidByCustomer { get; private set; }
        public bool HotelReservationConfirmed { get; private set; } = false;
        public string Flight { get; private set; }
        public decimal FlightFare { get; private set; }
        public decimal FlightFarePaidByCustomer { get; private set; }
        public bool FlightConfirmed { get; private set; } = false;
        public decimal TotalAmount { get; private set; }
        public bool Confirmed { get; private set; } = false;

        public static Trip Create(Guid correlationId, string name)
        {
            Trip trip = new() { Name = name, CorrelationId = correlationId };

            trip.SetSecondaryId(correlationId);

            trip.Raise(new TripCreatedDomainEvent(trip.GlobalUId, trip.CorrelationId, trip.Name));

            return trip;
        }

        public void BookHotel(string hotel, decimal reservationAmount, decimal amountPaidByCutomer)
        {
            ValidateServiceAmount(reservationAmount, amountPaidByCutomer);

            Hotel = hotel;
            HotelReservation = reservationAmount;
            HotelReservationPaidByCustomer = amountPaidByCutomer;
            HotelReservationConfirmed = true;

            Raise(new HotelReservationConfirmedDomainEvent(GlobalUId, Hotel, HotelReservation, HotelReservationPaidByCustomer, HotelReservationConfirmed));
        }

        public void CancelHotelReservation()
        {
            HotelReservationPaidByCustomer = 0;
            HotelReservationConfirmed = false;

            Raise(new HotelReservationCancelledDomainEvent(GlobalUId, HotelReservationPaidByCustomer, HotelReservationConfirmed));
        }

        public void BookFlight(string flight, decimal fare, decimal amountPaidByCustomer)
        {
            ValidateServiceAmount(fare, amountPaidByCustomer);

            Flight = flight;
            FlightFare = fare;
            FlightFarePaidByCustomer = amountPaidByCustomer;
            FlightConfirmed = true;

            Raise(new FlightReservationConfirmedDomainEvent(GlobalUId, Flight, FlightFare, FlightFarePaidByCustomer, FlightConfirmed));
        }

        public void CancelFlight()
        {
            FlightFarePaidByCustomer = 0;
            FlightConfirmed = false;

            Raise(new FlightReservationCancelledDomainEvent(GlobalUId, FlightFarePaidByCustomer, FlightConfirmed));
        }

        public void ConfirmTrip()
        {
            decimal amountPaid = HotelReservationPaidByCustomer + FlightFarePaidByCustomer;

            ValidateTotalAmount(TotalAmount, amountPaid);

            decimal totalAmount = HotelReservation + FlightFare;

            SetTotalAmount(totalAmount);

            Confirmed = true;

            Raise(new TripConfirmedDomainEvent(GlobalUId, TotalAmount, Confirmed));
        }

        public void CancelTrip()
        {
            CancelFlight();
            CancelHotelReservation();

            Confirmed = false;

            Raise(new TripCancelledDomainEvent(GlobalUId, Confirmed));
        }

        private void SetTotalAmount(decimal amount)
        {
            TotalAmount += amount;
        }

        private void ValidateServiceAmount(decimal serviceAmount, decimal amountPaidByCustomer)
        {
            if (amountPaidByCustomer < serviceAmount)
            {
                ThrowDomainException("The paid amount is less than the amount required for this service.");
            }
        }

        private void ValidateTotalAmount(decimal totalAmount, decimal amountPaid)
        {
            if (amountPaid < totalAmount)
            {
                ThrowDomainException("The paid amount is less than the amount required for this trip.");
            }
        }

        private void Apply(TripCreatedDomainEvent @event)
        {
            CorrelationId = @event.CorrelationId;
            Name = @event.Name;
        }

        private void Apply(HotelReservationConfirmedDomainEvent @event)
        {
            Hotel = @event.Hotel;
            HotelReservation = @event.HotelReservation;
            HotelReservationPaidByCustomer = @event.HotelReservationPaidByCustomer;
            HotelReservationConfirmed = @event.HotelReservationConfirmed;
        }

        private void Apply(HotelReservationCancelledDomainEvent @event)
        {
            HotelReservationPaidByCustomer = @event.HotelReservationPaidByCustomer;
            HotelReservationConfirmed = @event.HotelReservationConfirmed;
        }

        private void Apply(FlightReservationConfirmedDomainEvent @event)
        {
            Flight = @event.Flight;
            FlightFare = @event.FlightFare;
            FlightFarePaidByCustomer = @event.FlightFarePaidByCustomer;
            FlightConfirmed = @event.FlightConfirmed;
        }

        private void Apply(FlightReservationCancelledDomainEvent @event)
        {
            FlightFarePaidByCustomer = @event.FlightFarePaidByCustomer;
            FlightConfirmed = @event.FlightConfirmed;
        }

        private void Apply(TripConfirmedDomainEvent @event)
        {
            TotalAmount = @event.TotalAmount;
            Confirmed = @event.Confirmed;
        }

        private void Apply(TripCancelledDomainEvent @event)
        {
            Confirmed = @event.Confirmed;
        }
    }
}
