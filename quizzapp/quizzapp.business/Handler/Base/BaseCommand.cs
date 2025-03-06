using System;
using MediatR;
using quizzapp.model.Base;

namespace quizzapp.business.Handler.Base;

public abstract class BaseCommand<TResponse> : IRequest<TResponse>
{

}
