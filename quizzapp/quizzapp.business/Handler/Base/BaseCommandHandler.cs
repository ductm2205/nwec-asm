using System;
using MediatR;
using quizzapp.data.Infrastructure;

namespace quizzapp.business.Handler.Base;

public abstract class BaseCommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, TResponse> where TCommand : BaseCommand<TResponse>
{

    protected readonly IUnitOfWork _unitOfWork;

    public BaseCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<TResponse> Handle(TCommand request, CancellationToken cancellationToken)
    {
        return await HandleCommand(request, cancellationToken);
    }

    protected abstract Task<TResponse> HandleCommand(TCommand request, CancellationToken cancellationToken);
}
