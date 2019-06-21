using System;
using AutoMapper;
using Common.Api.Extensions;
using Common.Api.Formats;
using Common.Domain.DomainEvents;
using Common.Domain.DomainEvents.Interfaces;
using Common.Domain.Models.DataProtection;
using Common.Messaging.Infrastructure;
using Common.Messaging.Infrastructure.Interfaces;
using Common.Setup;
using Common.Setup.Infrastructure.Logging;
using Logsheet.Setup;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using EventRegistration = Logsheet.Setup.EventRegistration;
using MessageRegistration = Logsheet.Setup.MessageRegistration;
using ServiceRegistration = Logsheet.Setup.ServiceRegistration;

[assembly: ApiController]
namespace Logsheet.Api
{
    public class Startup : Common.Api.Startup
    {
        public Startup(IHostingEnvironment environment) : base(environment)
        {
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            CommonConfigureServices(services);

            services.Configure<MvcOptions>(options =>
            {
                options
                    .UseConcurrencyFilter()
                    .UseBusinessExceptionFilter()
                    .UseValidationExceptionFilter()
                    .RequireHttps();
            });

            FormatRegistration.ConfigureJsonV1Format(services);

            JwtRegistration.RegisterOptions(Configuration, services);

            MessageRegistration.RegisterSubscribers(services);

            EventRegistration.RegisterHandlers(services);

            //TODO: Review making into extension methods 
            ServiceRegistration.RegisterBuilders(services);
            ServiceRegistration.RegisterValidators(services);
            ServiceRegistration.RegisterApplicationServices(services);
            ServiceRegistration.RegisterDomainServices(services);
            ServiceRegistration.RegisterPersistenceServices(Configuration, services);
            ServiceRegistration.RegisterProviders(services);
            ServiceRegistration.RegisterAuthorisation(services);
            ServiceRegistration.RegisterNotifications(services);

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            return services.BuildServiceProvider();
        }

        public void Configure(IApplicationBuilder app,
            ILoggerFactory loggerFactory,
            IDataProtector dataProtector,
            IDomainEventDispatcher domainEventDispatcher,
            IMessageSender messageSender)
        {
            DataProtection.SetDataProtector(dataProtector);
            DomainEvents.SetDomainEventDispatcher(domainEventDispatcher);
            Message.SetMessageSender(messageSender);

            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            loggerFactory.AddLog4Net();

            MiddlewareRegistration.Register(app);

            app.UseHttpsRedirection();
            app.UseHsts();
            app.UseMvc();
        }
    }
}