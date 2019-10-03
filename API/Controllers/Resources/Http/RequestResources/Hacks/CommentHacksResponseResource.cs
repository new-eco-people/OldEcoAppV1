using System;

namespace API.Controllers.Resources.Http.RequestResources.Hacks
{
    public class CommentHacksResponseResource
    {
        public string Message { get; set; }
        public DateTime DateCreated { get; set; }
        public UserHacksResponseResource User { get; set; }
    }
}