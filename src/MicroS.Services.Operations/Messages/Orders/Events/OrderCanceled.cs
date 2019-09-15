using MicroS_Common.Messages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MicroS.Services.Operations.Messages.Orders.Events
{
    [MessageNamespace("orders")]
    public class OrderCanceled : IEvent
    {
        public Guid Id { get; }
        public Guid CustomerId { get; }
        public IDictionary<Guid, int> Products { get; }

        [JsonConstructor]
        public OrderCanceled(Guid id, Guid customerId, IDictionary<Guid, int> products)
        {
            Id = id;
            CustomerId = customerId;
            Products = products;
        }
    }
}
