using MicroS_Common.Messages;
using Newtonsoft.Json;
using System;

namespace MicroS.Services.Operations.Messages.Orders.Events
{
    public class OrderRevoked : IEvent
    {
        public Guid Id { get; }
        public Guid CustomerId { get; }

        [JsonConstructor]
        public OrderRevoked(Guid id, Guid customerId)
        {
            Id = id;
            CustomerId = customerId;
        }
    }
}
