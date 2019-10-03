using System;
using System.ComponentModel.DataAnnotations;

namespace API.Controllers.Resources.Http.RequestResources.Projects
{
    public class EditProjectRequestResource
    {
        public EditProjectRequestResource()
        {
            DateModified = DateTime.Now;
        }

        [Required, StringLength(255, MinimumLength = 2, ErrorMessage = "Topic should be between 2 and 255")]
        public string Title { get; set; }

        [Required, StringLength(1000, MinimumLength = 2, ErrorMessage = "Description should be between 2 and 1000")]
        public string Description { get; set; }
        public DateTime DateModified { get; private set; }
    }
}