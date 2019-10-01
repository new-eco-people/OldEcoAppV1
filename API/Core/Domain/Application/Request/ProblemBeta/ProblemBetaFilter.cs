using API.Core.Domain.Application.Request.Generic;

namespace API.Core.Domain.Models.Application.Request.ProblemBeta
{
    public class ProblemBetaFilter : QueryResult
    {
        public string Name { get; set; }
        public int? CountryId { get; set; }
        public int? StateId { get; set; }
        public string Eco { get; set; }
        public string Ico { get; set; }
        public string EcoUn { get; set; }
    }
}