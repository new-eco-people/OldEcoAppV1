using System;
using System.ComponentModel.DataAnnotations;
using API.Helper.Validations.Annotations;

namespace API.Controllers.Resources.Http.RequestResources.Problems
{
    public class CreateProblemRequestResource
    {

        [Required, StringLength(50), IsEcoClassAvaliable(ErrorMessage="Invalid Class chosen")]
        public string Class { get; set; }

        [Required, StringLength(50)]
        public string Subclass { get; set; }

        [Required, StringLength(255)]
        public string Location { get; set; }

        // Type of problem : Environmental Community or Organization
        //      
        // 
        [Required, StringLength(255)]
        public string Topic { get; set; }

        // Picture or video
        // 
        // what a user need to solve the problem

        [Required, StringLength(3000)]
        public string Description { get; set; }
        public DateTime DateCreated { get; private set; }

        public CreateProblemRequestResource()
        {
            DateCreated = DateTime.Now;
        }
    }
}