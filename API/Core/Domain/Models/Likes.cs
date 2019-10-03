

using System;

namespace API.Core.Domain.Models
{
    public class Likes
    {
        public Guid Id { get; set; }
        public ProblemBeta ProblemBeta { get; set; }
        public Guid ProblemBetaId { get; set; }
        public User User { get; set; }
        public Guid UserId { get; set; }
    }
}
