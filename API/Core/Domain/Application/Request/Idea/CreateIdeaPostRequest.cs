using System;
using System.Collections.Generic;

namespace API.Core.Domain.Application.Request.Idea
{
    public class CreateIdeaPostRequest
    {
        public string Description { get; set; }
        public string Eco { get; set; }
        public string EcoUn { get; set; }
        public string EcoUnOther { get; set; }
        public string Ico { get; set; }
        public string IcoOther { get; set; }
        public int? CountryId { get; set; }
        public int? StateId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        // Leave as default for now
        public string Email { get; set; }
        public string ProjectLink { get; set; }
        public List<byte[]> RealImages { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}