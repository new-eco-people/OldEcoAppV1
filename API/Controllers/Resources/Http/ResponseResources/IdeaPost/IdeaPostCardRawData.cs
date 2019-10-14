namespace API.Controllers.Resources.Http.ResponseResources.IdeaPost
{
    public class IdeaPostCardRawData
    {
        public API.Core.Domain.Models.IdeaPost IdeaPost { get; set; }
        public int Likes { get; set; }
        public int Comments { get; set; }
        public int Ideas { get; set; }
    }
}