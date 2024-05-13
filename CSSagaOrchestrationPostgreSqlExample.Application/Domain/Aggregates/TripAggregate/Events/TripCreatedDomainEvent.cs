using System;
using Newtonsoft.Json;
using CrystalSharp.Domain.Infrastructure;

namespace CSSagaOrchestrationPostgreSqlExample.Application.Domain.Aggregates.TripAggregate.Events
{
    public class TripCreatedDomainEvent : DomainEvent
    {
        public Guid CorrelationId { get; set; }
        public string Name { get; set; }

        public TripCreatedDomainEvent(Guid streamId, Guid correlationId, string name)
        {
            StreamId = streamId;
            CorrelationId = correlationId;
            Name = name;
        }

        [JsonConstructor]
        public TripCreatedDomainEvent(Guid streamId,
            Guid correlationId,
            string name,
            int entityStatus,
            DateTime createdOn,
            DateTime? modifiedOn,
            long version)
        {
            StreamId = streamId;
            CorrelationId = correlationId;
            Name = name;
            EntityStatus = entityStatus;
            CreatedOn = createdOn;
            ModifiedOn = modifiedOn;
            Version = version;
        }
    }
}
