using MediatR;

namespace business.Commands;

public interface IHasIdCommand<T> : IRequest<T>
{
    public Guid Id { get; set; }
}