using System;
using Newtonsoft.Json;
using CrystalSharp.Domain.Infrastructure;

namespace CSSagaOrchestrationPostgreSqlExample.Application.Domain.Aggregates.TripAggregate.Events
{
    public class TripConfirmedDomainEvent : DomainEvent
    {
        public decimal TotalAmount { get; private set; }
        public bool Confirmed { get; private set; }

        public TripConfirmedDomainEvent(Guid streamId, decimal totalAmount, bool confirmed)
        {
            StreamId = streamId;
            TotalAmount = totalAmount;
            Confirmed = confirmed;
        }

        [JsonConstructor]
        public TripConfirmedDomainEvent(Guid streamId,
            decimal totalAmount,
            bool confirmed,
            int entityStatus,
            DateTime createdOn,
            DateTime? modifiedOn,
            long version)
        {
            StreamId = streamId;
            TotalAmount = totalAmount;
            Confirmed = confirmed;
            EntityStatus = entityStatus;
            CreatedOn = createdOn;
            ModifiedOn = modifiedOn;
            Version = version;
        }
    }
}
