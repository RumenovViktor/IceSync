namespace IceSync.BL.Commands.Synchronize;

using MediatR;

public record SynchronizeCommand : IRequest<Unit>;