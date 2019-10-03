using System;
using System.Collections.Generic;
using API.Controllers.Resources.Http.ResponseResources.Comments;
using API.Controllers.Resources.Http.ResponseResources.Location;
using API.Controllers.Resources.Http.ResponseResources.Photos;
using API.Controllers.Resources.Http.ResponseResources.Projects;
using API.Controllers.Resources.Http.ResponseResources.Users;

namespace API.Controllers.Resources.Http.ResponseResources.Problems
{
    public class ProblemBetaResponseResource
    {
        public Guid Id { get; set; }
        public DateTime DateCreated { get; set; }
        public string Description  { get; set; }
        public string Eco { get; set; }
        public string EcoUn { get; set; }
        public string EcoUnOther { get; set; }
        public string Ico { get; set; }
        public string IcoOther { get; set; }
        public CountryResponseResource Country { get; set; }
        public StateResponseResource State { get; set; }
        public UserResponseResource User { get; set; }
        public ICollection<ProblemPhotosResponseResource> Photos { get; set; }
        public ICollection<ProjectResponseResource> Projects { get; set; }
        // public ICollection<CommentResponseResource> Ideas { get; set; }
        // public ICollection<CommentResponseResource> Comments { get; set; }
        
    }
}