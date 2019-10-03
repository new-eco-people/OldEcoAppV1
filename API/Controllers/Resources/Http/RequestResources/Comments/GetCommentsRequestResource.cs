using System.ComponentModel.DataAnnotations;
using API.Helper.Validations.Annotations;

namespace API.Controllers.Resources.Http.RequestResources.Comments
{
    public class GetCommentsRequestResource
    {
        [Required(ErrorMessage = "It seems we cannot get the chats for this problem")]
        public string ProblemBetaId { get; set; }
        [ProblemPaging(1, 2000, 1)]
        public int? Page { get; set; }

        [ProblemPaging(1, 10, 10)]
        public int? PageSize { get; set; }
    }
}