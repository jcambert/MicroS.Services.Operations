using MicroS_Common.Messages;
using Newtonsoft.Json;
using System;

namespace MicroS.Services.Operations.Messages.Products.Events
{
    [MessageNamespace("products")]
    public class ProductDeleted : IEvent
    {
        public Guid Id { get; }

        [JsonConstructor]
        public ProductDeleted(Guid id)
        {
            Id = id;
        }
    }
}
