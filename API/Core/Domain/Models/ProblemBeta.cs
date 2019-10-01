using System;
using System.Collections.Generic;
using API.Core.Domain.Models.Defaults;

namespace API.Core.Domain.Models
{
    public class ProblemBeta
    {
        public Guid Id { get; set; }
         
        public string Description { get; set; }

        public string Eco { get; set; }
        public string EcoUn { get; set; }
        public string EcoUnOther { get; set; }
        public string Ico { get; set; }
        public string IcoOther { get; set; }
        public Country Country { get; set; }
        public int? CountryId { get; set; }
        public State State { get; set; }
        public int? StateId { get; set; }
        public Boolean Deleted { get; set; }

        public User User { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public ICollection<Project> Projects { get; set; }
        public ICollection<Photo> Photos { get; set; }
        public ICollection<Likes> Likes { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Idea> Ideas { get; set; }

        public ProblemBeta()
        {
            Projects = new List<Project>();
            Photos = new List<Photo>();
            Likes = new List<Likes>();
            Comments = new List<Comment>();
            Ideas = new List<Idea>();
            User = null;
        }

        // public IFormFile Pictures { get; set; }
    }
}