using API.Core.Domain.Models;

namespace API.Controllers.Resources.Internal
{
    public class InternalUser
    {
        public User User { get; set; }
        public string Token { get; set; }
    }
}