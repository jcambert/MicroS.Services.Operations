﻿using MicroS_Common.Messages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MicroS.Services.Operations.Messages.Products.Events
{
    [MessageNamespace("products")]
    public class ProductsReleased : IEvent
    {
        public Guid OrderId { get; set; }
        public IDictionary<Guid, int> Products { get; }

        [JsonConstructor]
        public ProductsReleased(Guid orderId, IDictionary<Guid, int> products)
        {
            OrderId = orderId;
            Products = products;
        }
    }
}
