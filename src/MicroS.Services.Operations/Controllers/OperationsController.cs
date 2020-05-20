using MicroS_Common.Dispatchers;
using MicroS_Common.Services.Operations.Controllers;
using MicroS_Common.Services.Operations.Services;
using Microsoft.Extensions.Configuration;

namespace MicroS.Services.Operations.Controllers
{

    public class OperationsController : BaseOperationsController
    {
        public OperationsController(IDispatcher dispatcher, IConfiguration config, IOperationsStorage operationsStorage) : base(dispatcher, config, operationsStorage)
        { }

    }
}
