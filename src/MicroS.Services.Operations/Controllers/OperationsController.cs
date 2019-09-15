using MicroS.Services.Operations.Dto;
using MicroS.Services.Operations.Services;
using MicroS_Common.Dispatchers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace MicroS.Services.Operations.Controllers
{
    [Route("[controller]")]
    public class OperationsController : BaseController
    {
        private readonly IOperationsStorage _operationsStorage;

        public OperationsController(IDispatcher dispatcher,
            IOperationsStorage operationsStorage) : base(dispatcher)
        {
            _operationsStorage = operationsStorage;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OperationDto>> Get(Guid id)
            => Single(await _operationsStorage.GetAsync(id));
    }
}
