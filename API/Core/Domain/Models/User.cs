using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace API.Core.Domain.Models
{
    public class User : IdentityUser<Guid>
    {
        // [StringLength(256)]
        // public override string UserName { get; set; }
        public PersonalDetail PersonalDetail { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<Project> Projects { get; set; }
        // public ICollection<Problem> Problems { get; set; }
        public ICollection<ProblemBeta> ProblemsBeta { get; set; }
        public Boolean Deleted { get; set; }
        public bool AgreeToTermsAndCondition { get; set; }

        public User()
        {
            UserRoles = new List<UserRole>(); 
            Projects = new List<Project>(); 
            // Problems = new List<Problem>();
            ProblemsBeta = new List<ProblemBeta>();
        }
    }
}