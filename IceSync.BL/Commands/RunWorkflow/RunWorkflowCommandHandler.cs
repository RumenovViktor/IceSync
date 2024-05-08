namespace IceSync.BL.Commands.RunWorkflow;

using MediatR;

public class RunWorkflowCommandHandler : IRequestHandler<RunWorkflowCommand, Unit>
{
    private readonly IExternalWorkflowsRepository _externalWorkflowsRepository;

    public RunWorkflowCommandHandler(IExternalWorkflowsRepository externalWorkflowsRepository)
    {
        _externalWorkflowsRepository = externalWorkflowsRepository;
    }

    public async Task<Unit> Handle(RunWorkflowCommand request, CancellationToken cancellationToken) => 
        await _externalWorkflowsRepository.Run(request.WorkflowId);
}