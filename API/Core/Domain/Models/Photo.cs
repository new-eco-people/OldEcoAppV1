using System;

namespace API.Core.Domain.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public ProblemBeta ProblemBeta { get; set; }
        public Guid ProblemBetaId { get; set; }
        public Guid IdeaPostId { get; set; }
        public string PublicId { get; set; }

        public Photo()
        {
            DateAdded = DateTime.Now;
        }
    }
}