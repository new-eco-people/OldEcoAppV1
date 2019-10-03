using System.Collections.Generic;

namespace API.Controllers.Resources.Http.ResponseResources.Generic
{
    public class QueryResultResponseResource<T>
    {
        public int TotalItems { get; set; }

        public IEnumerable<T> Items { get; set; }
    }
}