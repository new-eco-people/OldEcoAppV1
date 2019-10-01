using API.Controllers.Resources.Http.ResponseResources.Users;

namespace API.Controllers.Resources.Http.RequestResources.Hacks
{
    public class UserHacksResponseResource
    {
        
        // public string Id { get; set; }
        // public string Email { get; set; }
        public PersonalDetailResponseResource PersonalDetail { get; set; }
    }
}