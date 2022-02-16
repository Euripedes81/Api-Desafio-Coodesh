using System;
using System.Collections.Generic;

namespace ApiDesafioCoodesh.Entities
{
    public class Article
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
        public List<Launch> Launches { get; set; }
        public List<Event> Events { get; set; }

        public Article()
        {
            Launches = new List<Launch>();
            Events = new List<Event>();
        }

    }
}
