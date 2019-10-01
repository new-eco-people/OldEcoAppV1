using System.Collections.Generic;

namespace API.Core.Domain.Models.Application.Response
{
    public class QueryResult<T>
    {
        public int TotalItems { get; set; }

        public IEnumerable<T> Items { get; set; }
    }
}