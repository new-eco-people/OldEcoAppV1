using API.Controllers.Resources.Http.ResponseResources.Projects;
using API.Controllers.Resources.Http.ResponseResources.Users;

namespace API.Controllers.Resources.Http.ResponseResources.Problems
{
    public class ProblemCardBetaResponseResource
    {
        public ProblemBetaResponseResource Problem { get; set; }
        public int Likes { get; set; }
        public int Comments { get; set; }
        public int Ideas { get; set; }
    }
}