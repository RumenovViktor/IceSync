namespace IceSync.BL.Commands.Synchronize.Update;

using MediatR;

public class UpdateWorkflowsCommandHandler : IRequestHandler<UpdateWorkflowsCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateWorkflowsCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(UpdateWorkflowsCommand request, CancellationToken cancellationToken)
    {
        var forUpdate = request.External.IntersectBy(request.Internal.Select(x => x.WorkflowId), x => x.WorkflowId).ToList();

        if (!forUpdate.Any())
            return Unit.Value;
        
        await _unitOfWork.Workflows.Update(forUpdate);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}