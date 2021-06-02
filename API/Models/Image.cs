namespace API.Models
{
    public class Image
    {
        public string Id { get; set; }
        public string RepoDigest { get; set; }
        public string RepoTags { get; set; }
        public int Size { get; set; }
    }
}