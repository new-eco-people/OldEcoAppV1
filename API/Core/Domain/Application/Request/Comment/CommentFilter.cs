using API.Core.Domain.Application.Request.Generic;

namespace API.Core.Domain.Application.Request.Comment
{
    public class CommentFilter : QueryResult
    {
        public string ProblemBetaId { get; set; }
    }
}