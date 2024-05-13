using System;
using Newtonsoft.Json;
using CrystalSharp.Domain.Infrastructure;

namespace CSSagaOrchestrationPostgreSqlExample.Application.Domain.Aggregates.TripAggregate.Events
{
    public class TripCancelledDomainEvent : DomainEvent
    {
        public bool Confirmed { get; private set; }

        public TripCancelledDomainEvent(Guid streamId, bool confirmed)
        {
            StreamId = streamId;
            Confirmed = confirmed;
        }

        [JsonConstructor]
        public TripCancelledDomainEvent(Guid streamId,
            bool confirmed,
            int entityStatus,
            DateTime createdOn,
            DateTime? modifiedOn,
            long version)
        {
            StreamId = streamId;
            Confirmed = confirmed;
            EntityStatus = entityStatus;
            CreatedOn = createdOn;
            ModifiedOn = modifiedOn;
            Version = version;
        }
    }
}
