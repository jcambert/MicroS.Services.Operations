using Autofac;
using Chronicle;
using Consul;
using MicroS.Services.Operations.Handlers;
using MicroS_Common;
using MicroS_Common.Consul;
using MicroS_Common.Dispatchers;
using MicroS_Common.Handlers;
using MicroS_Common.Jeager;
using MicroS_Common.Mongo;
using MicroS_Common.Mvc;
using MicroS_Common.RabbitMq;
using MicroS_Common.Redis;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace MicroS.Services.Operations
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCustomMvc();
            //services.AddSwaggerDocs();
            services.AddConsul();
            services.AddJaeger();
            services.AddOpenTracing();
            services.AddRedis();
            services.AddChronicle();
            services.AddInitializers(typeof(IMongoDbInitializer));
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.GetEntryAssembly()).AsImplementedInterfaces();
           
            builder.AddDispatchers();
            builder.AddRabbitMq();
            builder.AddMongo();
            builder.RegisterGeneric(typeof(GenericEventHandler<>)).As(typeof(IEventHandler<>));
            builder.RegisterGeneric(typeof(GenericCommandHandler<>)).As(typeof(ICommandHandler<>));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime applicationLifetime, IStartupInitializer startupInitializer
            , IConsulClient consulClient)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAllForwardedHeaders();
            //app.UseSwaggerDocs();
            app.UseErrorHandler();
            app.UseServiceId();
            app.UseMvc();
            app.UseRabbitMq()
                .SubscribeAllMessages();

            var consulServiceId = app.UseConsul();
            applicationLifetime.ApplicationStopped.Register(() =>
            {
                consulClient.Agent.ServiceDeregister(consulServiceId);
            });

            startupInitializer.InitializeAsync();
        }
    }
}
