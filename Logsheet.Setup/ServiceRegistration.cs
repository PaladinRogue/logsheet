using Common.Api.Links;
using Common.Api.Routing;
using Common.Application.Authorisation.Policy;
using Common.Application.Transactions;
using Common.Domain.Persistence;
using FluentValidation;
using Logsheet.Persistence;
using Logsheet.Setup.Infrastrucuture.Authorisation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.EntityFramework.Infrastructure.Transactions;
using Persistence.EntityFramework.Repositories;

namespace Logsheet.Setup
{
    public class ServiceRegistration
    {
        public static void RegisterBuilders(IServiceCollection services)
        {
            services.AddSingleton<ILinkFactory, DefaultLinkFactory>();
        }

        public static void RegisterValidators(IServiceCollection services)
        {
            ValidatorOptions.LanguageManager.Enabled = false;
        }

        public static void RegisterApplicationServices(IServiceCollection services)
        {
        }

        public static void RegisterDomainServices(IServiceCollection services)
        {
        }

        public static void RegisterPersistenceServices(IConfiguration configuration, IServiceCollection services)
        {
            services.AddScoped(typeof(ICommandRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IQueryRepository<>), typeof(Repository<>));

            services.AddEntityFrameworkSqlServer().AddOptions()
                .AddDbContext<LogsheetDbContext>(options =>
                    options.UseLazyLoadingProxies()
                        .UseSqlServer(configuration.GetConnectionString("Default")));
            services.AddScoped<DbContext>(sp => sp.GetRequiredService<LogsheetDbContext>());
            services.AddScoped<ITransactionManager, EntityFrameworkTransactionManager>();
        }

        public static void RegisterProviders(IServiceCollection services)
        {
            services.AddSingleton<IRouteProvider<bool>, DefaultRouteProvider>();
        }

        public static void RegisterAuthorisation(IServiceCollection services)
        {
            services.AddSingleton<IAuthorisationPolicy, AlwaysAllowAuthorisationPolicy>();
        }

        public static void RegisterNotifications(IServiceCollection services)
        {
        }
    }
}