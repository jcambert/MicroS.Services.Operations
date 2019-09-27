using MicroS_Common.Messages;
using System;

namespace MicroS.Services.Operations.Messages.Settings.Events
{
    [MessageNamespace("settings")]
    public class CreateSettingRejected : BaseRejectedEvent
    {
        public CreateSettingRejected(Guid id, string reason, string code) : base(id, reason, code)
        {
        }
    }
}
