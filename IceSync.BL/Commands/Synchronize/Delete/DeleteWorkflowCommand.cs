namespace IceSync.BL.Commands.Synchronize.Delete;

using Domain;
using MediatR;

public record DeleteWorkflowCommand(
    IEnumerable<Workflow> External,
    IEnumerable<Workflow> Internal) : IRequest<Unit>;