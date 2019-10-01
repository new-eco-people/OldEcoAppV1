using System;

namespace API.Controllers.Resources.Http.ResponseResources.Projects
{
    public class ProjectResponseResource
    {
        // public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        // public string UserId { get; set; }
        public string WebLink { get; set; }
        public DateTime DateCreated { get; private set; }
        public DateTime DateModified { get; private set; }

    }
}