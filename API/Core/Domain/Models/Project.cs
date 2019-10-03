using System;
using System.Collections.Generic;

namespace API.Core.Domain.Models
{
    public class Project
    {
        public Guid Id { get; set; }
        public string WebLink { get; set; }
        // public Dictionary<string, string> SocialLink { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Boolean Deleted { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public User User { get; set; }
        public Guid UserId { get; set; }
        // public Problem Problem { get; set; }
        // public Guid ProblemId { get; set; }
    }
}