using System;
using System.ComponentModel.DataAnnotations;

namespace API.Controllers.Resources.Http.RequestResources.Ideas
{
    public class LikeIdeaPostRequestResource
    {
        [Required(ErrorMessage="It seems the Id of the problem is missing, kindly refresh the page")]
        public string ProblemId { get; set; }

        [Required(ErrorMessage="Your email address is required"), EmailAddress(ErrorMessage="Invalid Email Entered")]
        public string EmailAddress{ get; set; }

        [Required(ErrorMessage="Kindly check the remember me check box")]
        public Boolean RememberMe { get; set; }
        
        [Required]
        [Range(typeof(bool), "true", "true", ErrorMessage="You must agree to the terms and conditions")]
        public bool Agree { get; set; }
    }
}