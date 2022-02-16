using System;
using System.Collections.Generic;

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
        public List<Launches> Launchess { get; set; }
        public List<Events> Eventss { get; set; }

        public Articles()
        {
            Launchess = new List<Launches>();
            Eventss = new List<Events>();
        }

    }
}
