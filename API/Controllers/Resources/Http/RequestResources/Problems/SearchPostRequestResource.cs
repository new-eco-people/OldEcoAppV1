using API.Helper.Validations.Annotations;

namespace API.Controllers.Resources.Http.RequestResources.Problems
{
    public class SearchPostRequestResource
    {
        public string Name { get; set; }
        public int? CountryId { get; set; }
        public int? StateId { get; set; }
        public string Eco { get; set; }
        public string Ico { get; set; }
        public string EcoUn { get; set; }

        [ProblemPaging(1, 2000, 1)]
        public int? Page { get; set; }

        [ProblemPaging(1, 10, 10)]
        public int? PageSize { get; set; }
    }
}