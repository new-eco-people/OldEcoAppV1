using API.Core.Domain.Models;

namespace API.Controllers.Resources.Http.ResponseResources.Problems
{
    public class ProblemCardRawData
    {
        public ProblemBeta Problem { get; set; }
        public int Likes { get; set; }
        public int Comments { get; set; }
        public int Ideas { get; set; }
    }
}