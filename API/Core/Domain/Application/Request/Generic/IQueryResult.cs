namespace API.Core.Domain.Application.Request.Generic
{
    public interface IQueryResult
    {
        int? Page { get; set; }
        int? PageSize { get; set; }
    }
}