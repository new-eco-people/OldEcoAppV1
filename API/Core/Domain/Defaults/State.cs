using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Core.Domain.Models.Defaults
{
    public class State
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }
        public Country Country { get; set; }
        public int CountryId { get; set; }
        public ICollection<ProblemBeta> ProblemBetas { get; set; }

        public State()
        {
            ProblemBetas = new List<ProblemBeta>();
        }
    }
}