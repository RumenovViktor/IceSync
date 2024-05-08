namespace IceSync.BL.Commands.Synchronize.Update;

using Domain;
using MediatR;

public record UpdateWorkflowsCommand(
    IEnumerable<Workflow> External,
    IEnumerable<Workflow> Internal) : IRequest<Unit>;