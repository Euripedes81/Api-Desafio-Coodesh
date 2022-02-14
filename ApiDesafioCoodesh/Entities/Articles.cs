using System;

namespace ApiDesafioCoodesh.Entities
{
    public class Articles
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string ImageUrl { get; set; }
        public string NewsSite { get; set; }
        public string Summary { get; set; }
        public DateTime PublishedAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public bool Featured { get; set; }
        public Launches launches { get; set; }
        public Events events { get; set; }
    }
}
