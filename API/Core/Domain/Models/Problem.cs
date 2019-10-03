using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Core.Domain.Models
{
    public class Problem
    {
         public Guid Id { get; set; }

        // public string Class { get; set; }

        // public string Subclass { get; set; }

        public string Location { get; set; }

        // Type of problem : Environmental Community or Organization
        //      
        // 
        public string Topic { get; set; }

        // Picture or video
        // 
        // what a user need to solve the problem
        public string Description { get; set; }
        public int Likes { get; set; }
        public Boolean Deleted { get; set; }
        // public Guid UserId { get; set; }
        public User User { get; set; }
        // public ICollection<Idea> Ideas { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        // public ICollection<Project> Projects { get; set; }

        public Problem()
        {
            // Projects = new List<Project>();
        }
    }
}