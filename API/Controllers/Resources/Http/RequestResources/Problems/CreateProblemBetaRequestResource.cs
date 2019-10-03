using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using API.Core.Domain.Models;
using API.Helper.Validations.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;

namespace API.Controllers.Resources.Http.RequestResources.Problems
{
    public class CreateProblemBeteRequestResource
    {
        [Required, StringLength(1000, ErrorMessage="Description exceeds 1000 characters")]
        public string Description { get; set; }

        [Required, IsEcoClassAvaliable(ErrorMessage="Invalid Class chosen")]
        public string Eco { get; set; }

        [Required, isEcoUn, NullWhiteSpaces]
        public string EcoUn { get; set; }

        [StringLength(100, ErrorMessage="Other UN goals should be between 2 - 100 chars"), NullWhiteSpaces]
        public string EcoUnOther { get; set; }

        [Required, isIcoAvaliable]
        public string Ico { get; set; }

        [StringLength(100, ErrorMessage="Other ICO should be between 2 - 100 chars"), NullWhiteSpaces]
        public string IcoOther { get; set; }

        [Required, NotNull(ErrorMessage="Country cannot be empty")]
        public int? CountryId { get; set; }

        [Required, NotNull(ErrorMessage="State cannot be empty")]
        public int? StateId { get; set; }
        public IFormFile Pictures { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        // Leave as default for now
        [OptionalEmail]
        public string Email { get; set; }
        public Boolean? addProjectToProblem { get; set; }

        [Required]
        [Range(typeof(bool), "true", "true", ErrorMessage="You must agree to the terms and conditions")]
        public bool Agree { get; set; }

        [OptionalUrl(ErrorMessage="The project link is invalid")]
        public string ProjectLink { get; set; }

        [IsBase64Image("RealImages")]
        public List<string> Images { get; set; }
        public List<byte[]> RealImages { get; set; }
        public bool AddProblemToUser { get; set; }
        public bool RememberMe { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

        public CreateProblemBeteRequestResource() {
            // User = null;
            DateCreated = DateModified = DateTime.Now;
        }

    }
}