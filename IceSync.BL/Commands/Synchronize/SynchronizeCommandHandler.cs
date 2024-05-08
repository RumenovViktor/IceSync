namespace IceSync.BL.Commands.Synchronize;

using Delete;
using Insert;
using MediatR;
using Update;

public class SynchronizeCommandHandler : IRequestHandler<SynchronizeCommand, Unit>
{
    private readonly IExternalWorkflowsRepository _externalWorkflowsRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMediator _mediator;

    public SynchronizeCommandHandler(
        IExternalWorkflowsRepository externalWorkflowsRepository, 
        IUnitOfWork unitOfWork, 
        IMediator mediator)
    {
        _externalWorkflowsRepository = externalWorkflowsRepository;
        _unitOfWork = unitOfWork;
        _mediator = mediator;
    }

    public async Task<Unit> Handle(SynchronizeCommand request, CancellationToken cancellationToken)
    {
        var externalWorkflows = (await _externalWorkflowsRepository.Get()).ToList();
        var localWorkflows = (await _unitOfWork.Workflows.Get()).ToList();

        var insertCommand = new InsertWorkflowCommand(externalWorkflows, localWorkflows);
        var deleteCommand = new DeleteWorkflowCommand(externalWorkflows, localWorkflows);
        var updateCommand = new UpdateWorkflowsCommand(externalWorkflows, localWorkflows);

        var insertTask = _mediator.Send(insertCommand, cancellationToken);
        var deleteTask = _mediator.Send(deleteCommand, cancellationToken);
        var updateTask = _mediator.Send(updateCommand, cancellationToken);

        await Task.WhenAll(insertTask, deleteTask, updateTask);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}