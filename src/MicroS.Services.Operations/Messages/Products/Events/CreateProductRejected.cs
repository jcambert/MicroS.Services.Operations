using MicroS_Common.Messages;
using Newtonsoft.Json;
using System;

namespace MicroS.Services.Operations.Messages.Products.Events
{
    [MessageNamespace("products")]
    public class CreateProductRejected : BaseRejectedEvent
    {
        public CreateProductRejected(Guid id, string reason, string code) : base(id, reason, code)
        {
        }
    }
}
