using System;
using business.Commands;
using data.Infrastructures;
using MediatR;

namespace business.Handlers;

public abstract class BaseHandler<TCommand, TResponse> : IRequestHandler<TCommand, TResponse> where TCommand : BaseCommand<TResponse>
{
    protected readonly IUnitOfWork _unitOfWork;

    public BaseHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<TResponse> Handle(TCommand request, CancellationToken cancellationToken)
    {
        return await HandleCommand(request, cancellationToken);
    }

    protected abstract Task<TResponse> HandleCommand(TCommand request, CancellationToken cancellationToken);
}
