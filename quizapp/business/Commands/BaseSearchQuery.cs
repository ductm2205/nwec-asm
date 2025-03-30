using System;
using core.Models;
using core.Models.Responses;
using MediatR;

namespace business.Commands;

public class BaseSearchQuery<T> : BaseCommand<PaginatedResult<T>>
{
    public string? Keyword { get; set; }

    public int PageNumber { get; set; } = 1;

    public int PageSize { get; set; } = 10;

    public string? OrderBy { get; set; } = "CreatedAt";

    public OrderDirection OrderDirection { get; set; } = OrderDirection.ASC;
    public bool? IncludeInactive { get; set; }
    public bool? IncludeDeleted { get; set; }

}
public enum OrderDirection
{
    ASC,
    DESC
}
