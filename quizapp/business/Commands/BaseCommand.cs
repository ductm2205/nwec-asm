using MediatR;

namespace business.Commands;

public class BaseCommand<T> : IRequest<T>
{
}
