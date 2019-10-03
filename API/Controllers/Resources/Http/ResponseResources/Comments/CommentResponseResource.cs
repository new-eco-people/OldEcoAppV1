using System;
using API.Controllers.Resources.Http.ResponseResources.Users;

namespace API.Controllers.Resources.Http.ResponseResources.Comments
{
    public class CommentResponseResource
    {
        public string Message { get; set; }
        public DateTime DateCreated { get; set; }
        public UserResponseResource User { get; set; }
    }
}