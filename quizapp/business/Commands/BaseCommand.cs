using System;
using System.ComponentModel.DataAnnotations;
using MediatR;

namespace business.Commands;

public class BaseCommand<T> : IRequest<T>
{
}
