namespace IceSync.BL.Synchronization;

using Commands.Synchronize;
using Configurations;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

public class WorkflowSynchronizer : BackgroundService
{
    private readonly ILogger<WorkflowSynchronizer> _logger;
    private readonly IOptionsMonitor<WorkerSettings> _workerSettings;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public WorkflowSynchronizer( 
        ILogger<WorkflowSynchronizer> logger, 
        IOptionsMonitor<WorkerSettings> workerSettings,
        IServiceScopeFactory serviceScopeFactory)
    {
        _logger = logger;
        _workerSettings = workerSettings;
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var timer = new PeriodicTimer(_workerSettings.CurrentValue.Interval);

        do
        {
            try
            {
                using var scope = _serviceScopeFactory.CreateScope();
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                await mediator.Send(new SynchronizeCommand(), stoppingToken);
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error, $"Error processing synchronization: {e.Message}");
            }
        } while (await timer.WaitForNextTickAsync(stoppingToken));
    }
}