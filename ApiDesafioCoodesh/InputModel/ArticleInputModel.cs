using ApiDesafioCoodesh.Entities;
using System;
using System.Collections.Generic;

namespace ApiDesafioCoodesh.InputModel
{
    public class ArticleInputModel
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


    }
}
