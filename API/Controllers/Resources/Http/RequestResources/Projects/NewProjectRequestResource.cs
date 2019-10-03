using System;

namespace API.Controllers.Resources.Http.RequestResources.Projects
{
    public class NewProjectRequestResource
    {
        public NewProjectRequestResource()
        {
            DateCreated = DateTime.Now;
            DateModified = DateTime.Now;
        }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; private set; }
        public DateTime DateModified { get; private set; }
    }
}