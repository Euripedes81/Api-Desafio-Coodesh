using ApiDesafioCoodesh.Entities;
using System;

namespace ApiDesafioCoodesh.InputModel
{
    public class ArticlesInputModel
    {
        //public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string ImageUrl { get; set; }
        public string NewsSite { get; set; }
        public string Summary { get; set; }
        public DateTime PublishedAt { get; set; }
       // public DateTime UpdateAt { get; set; }
        public bool Featured { get; set; }
        public string LaunchesId { get; set; }
        public string EventsId { get; set; }
    }
}
