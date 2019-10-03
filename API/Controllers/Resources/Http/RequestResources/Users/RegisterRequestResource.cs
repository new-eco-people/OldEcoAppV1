using System.ComponentModel.DataAnnotations;

namespace API.Controllers.Resources.Http.RequestResources.Users
{
    public class RegisterRequestResource
    {
        // [Required, StringLength(255)]
        // public string UserName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }
        
        [Required, StringLength(255)]
        public string Password { get; set; }

        // public RegisterRequestResource()
        // {
        //     UserName = "null";
        // }
    }
}