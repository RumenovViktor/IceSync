namespace IceSync.BL;

using Microsoft.Extensions.DependencyInjection;
using Synchronization;

public static class RegisterServicesExtensions
{
    public static IServiceCollection AddBlServices(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddHostedService<WorkflowSynchronizer>()
            .AddMediatR(x => x.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
    }
}