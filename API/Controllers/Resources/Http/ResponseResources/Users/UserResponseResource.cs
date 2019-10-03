namespace API.Controllers.Resources.Http.ResponseResources.Users
{
    public class UserResponseResource
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public PersonalDetailResponseResource PersonalDetail { get; set; }
    }
}