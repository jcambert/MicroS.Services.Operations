using MicroS.Services.Operations.Messages.Operations.Events;
using MicroS_Common.Messages;
using MicroS_Common.RabbitMq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MicroS.Services.Operations
{
    public static class Subscriptions
    {
        private static readonly Assembly MessagesAssembly = typeof(Subscriptions).Assembly;

        private static readonly ISet<Type> ExcludedMessages = new HashSet<Type>(new[]
        {
            typeof(OperationPending),
            typeof(OperationCompleted),
            typeof(OperationRejected)
        });

        public static IBusSubscriber SubscribeAllMessages(this IBusSubscriber subscriber,params Assembly[] assemblies)
            => subscriber.SubscribeAllCommands(assemblies).SubscribeAllEvents(assemblies);

        private static IBusSubscriber SubscribeAllCommands(this IBusSubscriber subscriber, params Assembly[] assemblies)
            => subscriber.SubscribeAllMessages<ICommand>(nameof(IBusSubscriber.SubscribeCommand),assemblies);

        private static IBusSubscriber SubscribeAllEvents(this IBusSubscriber subscriber, params Assembly[] assemblies)
            => subscriber.SubscribeAllMessages<IEvent>(nameof(IBusSubscriber.SubscribeEvent),assemblies);

        private static IBusSubscriber SubscribeAllMessages<TMessage>
            (this IBusSubscriber subscriber, string subscribeMethod, params Assembly[] assemblies)
        {
            var ass = new List<Assembly>(assemblies);
            ass.Insert(0, MessagesAssembly);
            ass.ForEach(assembly =>
            {
                var messageTypes = assembly
                .GetTypes()
                .Where(t => t.IsClass && typeof(TMessage).IsAssignableFrom(t))
                .Where(t => !ExcludedMessages.Contains(t))
                .ToList();

                messageTypes.ForEach(mt => subscriber.GetType()
                    .GetMethod(subscribeMethod)
                    .MakeGenericMethod(mt)
                    .Invoke(subscriber,
                        new object[] { mt.GetCustomAttribute<MessageNamespaceAttribute>()?.Namespace, null, null }));
            });
            

            return subscriber;
        }
    }
}
