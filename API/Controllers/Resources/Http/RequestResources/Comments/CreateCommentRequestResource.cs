using System;
using System.ComponentModel.DataAnnotations;

namespace API.Controllers.Resources.Http.RequestResources.Comments
{
    public class CreateCommentRequestResource
    {

        [Required(ErrorMessage = "Your email is required"), EmailAddress(ErrorMessage = "Email address is invalid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Your comment is required"), StringLength(1000, ErrorMessage = "Message is more than 1000 chars")]
        public string Message { get; set; }

        [Required(ErrorMessage = "It seems the problem cannot be found")]
        public string ProblemId { get; set; }

        [Required]
        public bool RememberMe { get; set; }
        
        [Required]
        [Range(typeof(bool), "true", "true", ErrorMessage="You must agree to the terms and conditions")]
        public bool Agree { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

        public CreateCommentRequestResource()
        {
            DateCreated = DateTime.Now;
            DateModified = DateTime.Now;
        }
    }
}