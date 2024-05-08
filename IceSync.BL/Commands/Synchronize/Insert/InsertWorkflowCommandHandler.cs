namespace IceSync.BL.Commands.Synchronize.Insert;

using MediatR;

public class InsertWorkflowCommandHandler : IRequestHandler<InsertWorkflowCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public InsertWorkflowCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(InsertWorkflowCommand request, CancellationToken cancellationToken)
    {
        var forInsert = request.External.ExceptBy(request.Internal.Select(x => x.WorkflowId), i => i.WorkflowId).ToList();
        
        if (!forInsert.Any())
            return Unit.Value;
        
        await _unitOfWork.Workflows.Insert(forInsert);
        return Unit.Value;
    }
}