namespace API.Controllers.Resources.Http.ResponseResources.IdeaPost
{
    public class IdeaPostCardResponseResource
    {
        public IdeaPostResponseResource IdeaPost { get; set; }
        public int Likes { get; set; }
        public int Comments { get; set; }
        public int Ideas { get; set; }
    }
}