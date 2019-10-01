using System;
using System.ComponentModel.DataAnnotations;

namespace API.Controllers.Resources.Http.RequestResources.Problems
{
    public class LikeProblemBetaWithIdRequestResource
    {
        [Required(ErrorMessage = "It seems we cannot find the problem")]
        public string ProblemId { get; set; }
    }
}