namespace IceSync.BL.Commands.Synchronize.Insert;

using Domain;
using MediatR;

public record InsertWorkflowCommand(
    IEnumerable<Workflow> External,
    IEnumerable<Workflow> Internal) : IRequest<Unit>;