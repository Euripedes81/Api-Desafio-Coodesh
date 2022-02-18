using HttpClientDesafioCoodesh.Entities;
using HttpClientDesafioCoodesh.Repositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace HttpClientDesafioCoodesh
{
    public class Program
    {
        public static IArticleRepository articleRepository = new ArticleRepository();
        public static void Main(string[] args)
        {
            int start = Convert.ToInt32(args[0]); 
            CallWebAPIAsync(start).Wait();
        }
        static async Task CallWebAPIAsync(int start)
        {
           
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.spaceflightnewsapi.net");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                List<Article> articleList = new List<Article>();
                
                do
                {
                    HttpResponseMessage response = await client.GetAsync($"v3/articles?_limit=100&_sort=id&_start={start}");
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        articleList = JsonConvert.DeserializeObject<List<Article>>(apiResponse);

                        foreach (var article in articleList)
                        {                     
                             
                            article.Title = article.Title.Replace("'", "\\'");
                           // articleRepository.Inserir(article);
                            Console.WriteLine($"Salvando Id:{article.Id} Title: {article.Title} {article.PublishedAt} {article.UpdateAt}");

                        }                      
                       
                        
                    }
                    else
                    {
                        Console.WriteLine("Internal error!");
                    }
                    start += 100;
                } while (articleList.Count >= 100);
        }
    }
}
}
