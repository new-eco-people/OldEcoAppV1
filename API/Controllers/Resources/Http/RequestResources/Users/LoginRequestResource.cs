using System.ComponentModel.DataAnnotations;

namespace API.Controllers.Resources.Http.RequestResources.Users
{
    public class LoginRequestResource
    {
        [Required]
        public string EmailUsername { get; set; }

        [Required]
        public string Password { get; set; }
    }
}