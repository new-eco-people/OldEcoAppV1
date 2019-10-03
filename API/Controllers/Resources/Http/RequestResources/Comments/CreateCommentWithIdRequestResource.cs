using System;
using System.ComponentModel.DataAnnotations;

namespace API.Controllers.Resources.Http.RequestResources.Comments
{
    public class CreateCommentWithIdRequestResource
    {
        [Required(ErrorMessage = "Your comment is required"), StringLength(1000, ErrorMessage = "Message is more than 1000 chars")]
        public string Message { get; set; }

        [Required(ErrorMessage = "It seems the problem cannot be found")]
        public string ProblemId { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

        public CreateCommentWithIdRequestResource()
        {
            DateCreated = DateTime.Now;
            DateModified = DateTime.Now;
        }
    }
}