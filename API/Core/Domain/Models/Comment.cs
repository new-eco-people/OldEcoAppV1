using System;

namespace API.Core.Domain.Models
{
    public class Comment
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public User User { get; set; }
        public Guid UserId { get; set; }
        public ProblemBeta ProblemBeta { get; set; }
        public Guid ProblemBetaId { get; set; }
    }
}