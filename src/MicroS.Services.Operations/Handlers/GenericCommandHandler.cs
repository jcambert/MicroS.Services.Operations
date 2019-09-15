using Chronicle;
using MicroS.Services.Operations.Sagas;
using MicroS.Services.Operations.Services;
using MicroS_Common.Handlers;
using MicroS_Common.Messages;
using MicroS_Common.RabbitMq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SagaContext = MicroS.Services.Operations.Sagas.SagaContext;

namespace MicroS.Services.Operations.Handlers
{
    public class GenericCommandHandler<T> : ICommandHandler<T> where T : class, ICommand
    {
        private readonly ISagaCoordinator _sagaCoordinator;
        private readonly IOperationPublisher _operationPublisher;
        private readonly IOperationsStorage _operationsStorage;

        public GenericCommandHandler(ISagaCoordinator sagaCoordinator,
            IOperationPublisher operationPublisher,
            IOperationsStorage operationsStorage)
        {
            _sagaCoordinator = sagaCoordinator;
            _operationPublisher = operationPublisher;
            _operationsStorage = operationsStorage;
        }

        public async Task HandleAsync(T command, ICorrelationContext context)
        {
            if (!command.BelongsToSaga())
            {
                return;
            }

            var sagaContext = SagaContext.FromCorrelationContext(context);
            await _sagaCoordinator.ProcessAsync(command, sagaContext);
        }
    }
}
