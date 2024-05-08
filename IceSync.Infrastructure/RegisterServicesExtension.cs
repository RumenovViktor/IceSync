namespace IceSync.Infrastructure;

using Authorization;
using BL;
using BL.Configurations;
using Clients;
using Clients.AuthHandlers;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using Repositories;

public static class RegisterServicesExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection serviceCollection, DatabaseSettings databaseSettings)
    {
        return serviceCollection
            .AddSingleton<IAccessToken, AccessToken>()
            .AddScoped<IExternalWorkflowsRepository, ExternalWorkflowsRepository>()
            .AddScoped<IWorkflowRepository, WorkflowRepository>()
            .AddScoped<IUnitOfWork, UnitOfWork>()
            .AddDatabase(databaseSettings)
            .AddRefitClient();
    }
    
    private static IServiceCollection AddRefitClient(this IServiceCollection serviceCollection)
    {
        _ = serviceCollection.BuildClient<IAuthClient>();
        
        _ = serviceCollection
            .BuildClient<IWorkflowsClient>()
            .AddHttpMessageHandler<BearerTokenHeaderDelegate>();
        
        serviceCollection.AddScoped<BearerTokenHeaderDelegate>();

        return serviceCollection;
    }

    private static IServiceCollection AddDatabase(
        this IServiceCollection serviceCollection,
        DatabaseSettings databaseSettings)
    {
        return serviceCollection
            .AddNpgsql<IceSyncDbContext>(databaseSettings.ConnectionString);
    }

    private static IHttpClientBuilder BuildClient<T>(this IServiceCollection serviceCollection) where T : class
    {
        return serviceCollection
            .AddRefitClient<T>()
            .ConfigureHttpClient(x => x.BaseAddress = new Uri("https://api-test.universal-loader.com"));
    }
}