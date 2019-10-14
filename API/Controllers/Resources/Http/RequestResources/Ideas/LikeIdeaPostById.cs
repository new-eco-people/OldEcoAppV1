using System.ComponentModel.DataAnnotations;

namespace API.Controllers.Resources.Http.RequestResources.Ideas
{
    public class LikeIdeaPostById
    {
        [Required(ErrorMessage = "It seems we cannot find the problem")]
        public string ProblemId { get; set; }
    }
    
}