namespace IceSync.BL.Commands.RunWorkflow;

using MediatR;

public record RunWorkflowCommand(int WorkflowId) : IRequest<Unit>;