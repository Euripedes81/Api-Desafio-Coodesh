using ApiDesafioCoodesh.Entities;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiDesafioCoodesh.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly MySqlConnection mySqlConnection;

        public ArticleRepository(IConfiguration configuration)
        {
            mySqlConnection = new MySqlConnection(@"server=x8autxobia7sgh74.cbetxkdyhwsb.us-east-1.rds.amazonaws.com;database=of0pkqgq9unpjqua;uid=sbpkruojpnkxzhol;pwd=irdicmhft81rqc3o;convert zero datetime=True");
            //mySqlConnection = new MySqlConnection(@"server=yjo6uubt3u5c16az.cbetxkdyhwsb.us-east-1.rds.amazonaws.com;
            //        database=lckckfanuue501ed;uid=y04qwys3d1pfe3ai;pwd=giw8ybslfeqf3u9o;convert zero datetime=True");
        }
        public async Task Atualizar(Article articles)
        {
           var comando = $"update article set  title = '{articles.Title}', url = '{articles.Url}', imageUrl = '{articles.ImageUrl}'," +
                $" newsSite = '{articles.NewsSite}', summary = '{articles.Summary}', publishedAt = '{articles.PublishedAt}'," +
                $" featured = '{articles.Featured}' where id = '{articles.Id}'";
            await mySqlConnection.OpenAsync();
            MySqlCommand mySqlCommand = new MySqlCommand(comando, mySqlConnection);
            mySqlCommand.ExecuteNonQuery();
            await mySqlConnection.CloseAsync();
        }

        //public void Dispose()
        //{
        //    throw new NotImplementedException();
        //}

        public async Task Inserir(Article articles)
        {
            var comando = "insert into article (id, title, url, imageUrl, newsSite, summary, publishedAt, featured)" +
                $" values ('{articles.Id}', '{articles.Title}', '{articles.Url}', '{articles.ImageUrl}', '{articles.NewsSite}', '{articles.Summary}', '{articles.PublishedAt}', '{articles.Featured}')";
            await mySqlConnection.OpenAsync();
            MySqlCommand mySqlCommand = new MySqlCommand(comando, mySqlConnection);
            mySqlCommand.ExecuteNonQuery();
            await mySqlConnection.CloseAsync();            

        }

        public async Task<List<Article>> Obter(int pagina, int quantidade)
        {
            var articles = new List<Article>();
            var comando = $"select * from article order by id  limit {quantidade} offset {((pagina - 1) * quantidade)}";
            await mySqlConnection.OpenAsync();
            MySqlCommand mySqlCommand = new MySqlCommand(comando, mySqlConnection);
            MySqlDataReader mySqlDataReader = (MySqlDataReader)await mySqlCommand.ExecuteReaderAsync();            
            while (mySqlDataReader.Read())
            {
                articles.Add(new Article
                {
                    Id = (int)mySqlDataReader["id"],
                    Title = (string)mySqlDataReader["title"],
                    Url = (string)mySqlDataReader["url"],
                    ImageUrl = (string)mySqlDataReader["imageUrl"],
                    NewsSite = (string)mySqlDataReader["newsSite"],
                    Summary = (string)mySqlDataReader["summary"],
                    PublishedAt = Convert.ToDateTime(mySqlDataReader["publishedAt"]),
                   // UpdateAt = Convert.ToDateTime(mySqlDataReader["updatedAt"]),
                    Featured = Convert.ToBoolean(mySqlDataReader["featured"]),                    

                });
            }
            await mySqlConnection.CloseAsync();
           
            List<Article> articlesAux = new List<Article>();

            foreach(var article in articles)
            {
                article.Launches = ObterLaunches(article.Id);
                article.Events = ObterEvents(article.Id);
                articlesAux.Add(article);
            }
            return articlesAux;
        }

        public async Task<Article> Obter(int id)
        {
            Article article = null;
            var comando = $"select * from article where id = '{id}'";
            await mySqlConnection.OpenAsync();
            MySqlCommand mySqlCommand = new MySqlCommand(comando, mySqlConnection);
            MySqlDataReader mySqlDataReader = (MySqlDataReader)await mySqlCommand.ExecuteReaderAsync();
            if (mySqlDataReader.Read())
            {                
                article = new Article
                {
                    Id = (int)mySqlDataReader["id"],
                    Title = (string)mySqlDataReader["title"],
                    Url = (string)mySqlDataReader["url"],
                    ImageUrl = (string)mySqlDataReader["imageUrl"],
                    NewsSite = (string)mySqlDataReader["newsSite"],
                    Summary = (string)mySqlDataReader["url"],
                    PublishedAt = (DateTime)mySqlDataReader["publishedAt"],
                    //UpdateAt = (DateTime)mySqlDataReader["updatedAt"],
                    Featured = Convert.ToBoolean(mySqlDataReader["featured"]),
                    
                };           
                await mySqlConnection.CloseAsync();                             
                article.Launches = ObterLaunches(article.Id);
                article.Events = ObterEvents(article.Id);                 
            }
            return article;

        }

        public async Task Remover(int id)
        {
            var comando = $"delete from article where id = '{id}'";
            await mySqlConnection.OpenAsync();
            MySqlCommand mySqlCommand = new MySqlCommand(comando, mySqlConnection);
            mySqlCommand.ExecuteNonQuery();
            await mySqlConnection.CloseAsync();
        }

        public List<Launch> ObterLaunches(int idArticle)
        {
            var launches = new List<Launch>();
            var comando = $"select * from launch where id = '{idArticle}'";
            mySqlConnection.Open();
            MySqlCommand mySqlCommand = new MySqlCommand(comando, mySqlConnection);
            MySqlDataReader mySqlDataReader = (MySqlDataReader) mySqlCommand.ExecuteReader();
            while (mySqlDataReader.Read())
            {
                launches.Add(new Launch
                {                   
                    Id = (string)mySqlDataReader["id"],
                    Provider = (string)mySqlDataReader["provider"]                   

                });
            }
            mySqlConnection.Close();

            return launches;
        }
        public List<Event> ObterEvents(int idArticle)
        {
            var events = new List<Event>();
            var comando = $"select * from event where id = '{idArticle}'";
            mySqlConnection.Open();
            MySqlCommand mySqlCommand = new MySqlCommand(comando, mySqlConnection);
            MySqlDataReader mySqlDataReader = (MySqlDataReader)mySqlCommand.ExecuteReader();
            while (mySqlDataReader.Read())
            {
                events.Add(new Event
                {
                    Id = (string)mySqlDataReader["id"],
                    Provider = (string)mySqlDataReader["provider"]

                });
            }
            mySqlConnection.Close();

            return events;
        }
    }
}
