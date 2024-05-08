namespace IceSync.BL.Commands.Synchronize.Delete;

using MediatR;

public class DeleteWorkflowCommandHandler : IRequestHandler<DeleteWorkflowCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteWorkflowCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Unit> Handle(DeleteWorkflowCommand request, CancellationToken cancellationToken)
    {
        var forDelete = request.Internal
            .ExceptBy(request.External.Select(x => x.WorkflowId), x => x.WorkflowId)
            .Select(x => x.WorkflowId)
            .ToList();
        
        if (!forDelete.Any())
            return Unit.Value;
        
        await _unitOfWork.Workflows.Delete(forDelete);
        return Unit.Value;
    }
}