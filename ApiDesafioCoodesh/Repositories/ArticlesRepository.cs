using ApiDesafioCoodesh.Entities;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiDesafioCoodesh.Repositories
{
    public class ArticlesRepository : IArticlesRepository
    {
        private readonly MySqlConnection mySqlConnection;

        public ArticlesRepository(IConfiguration configuration)
        {
            mySqlConnection = new MySqlConnection(@"server=x8autxobia7sgh74.cbetxkdyhwsb.us-east-1.rds.amazonaws.com;database=of0pkqgq9unpjqua;uid=sbpkruojpnkxzhol;pwd=irdicmhft81rqc3o;convert zero datetime=True");
            //mySqlConnection = new MySqlConnection(@"server=yjo6uubt3u5c16az.cbetxkdyhwsb.us-east-1.rds.amazonaws.com;
            //        database=lckckfanuue501ed;uid=y04qwys3d1pfe3ai;pwd=giw8ybslfeqf3u9o;convert zero datetime=True");
        }
        public async Task Atualizar(Articles articles)
        {
           var comando = $"update articles set  title = '{articles.Title}', url = '{articles.Url}', imageUrl = '{articles.ImageUrl}'," +
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

        public async Task Inserir(Articles articles)
        {
            var comando = "insert into articles (id, title, url, imageUrl, newsSite, summary, publishedAt, featured)" +
                $" values ('{articles.Id}', '{articles.Title}', '{articles.Url}', '{articles.ImageUrl}', '{articles.NewsSite}', '{articles.Summary}', '{articles.PublishedAt}', '{articles.Featured}')";
            await mySqlConnection.OpenAsync();
            MySqlCommand mySqlCommand = new MySqlCommand(comando, mySqlConnection);
            mySqlCommand.ExecuteNonQuery();
            await mySqlConnection.CloseAsync();            

        }

        public async Task<List<Articles>> Obter(int pagina, int quantidade)
        {
            var articless = new List<Articles>();
            var comando = $"select * from articles order by id  limit {quantidade} offset {((pagina - 1) * quantidade)}";
            await mySqlConnection.OpenAsync();
            MySqlCommand mySqlCommand = new MySqlCommand(comando, mySqlConnection);
            MySqlDataReader mySqlDataReader = (MySqlDataReader)await mySqlCommand.ExecuteReaderAsync();            
            while (mySqlDataReader.Read())
            {
                articless.Add(new Articles
                {
                    Id = (int)mySqlDataReader["id"],
                    Title = (string)mySqlDataReader["title"],
                    Url = (string)mySqlDataReader["url"],
                    ImageUrl = (string)mySqlDataReader["imageUrl"],
                    NewsSite = (string)mySqlDataReader["newsSite"],
                    Summary = (string)mySqlDataReader["url"],
                    PublishedAt = Convert.ToDateTime(mySqlDataReader["publishedAt"]),
                    UpdateAt = Convert.ToDateTime(mySqlDataReader["updatedAt"]),
                    Featured = Convert.ToBoolean(mySqlDataReader["featured"]),                    

                });
            }
            await mySqlConnection.CloseAsync();
           
            return articless;
        }

        public async Task<Articles> Obter(int id)
        {
            Articles articles = null;
            var comando = $"select * from articles where id = '{id}'";
            await mySqlConnection.OpenAsync();
            MySqlCommand mySqlCommand = new MySqlCommand(comando, mySqlConnection);
            MySqlDataReader mySqlDataReader = (MySqlDataReader)await mySqlCommand.ExecuteReaderAsync();
            if (mySqlDataReader.Read())
            {                
                articles = new Articles
                {
                    Id = (int)mySqlDataReader["id"],
                    Title = (string)mySqlDataReader["title"],
                    Url = (string)mySqlDataReader["url"],
                    ImageUrl = (string)mySqlDataReader["imageUrl"],
                    NewsSite = (string)mySqlDataReader["newsSite"],
                    Summary = (string)mySqlDataReader["url"],
                    PublishedAt = (DateTime)mySqlDataReader["publishedAt"],
                    UpdateAt = (DateTime)mySqlDataReader["updatedAt"],
                    Featured = Convert.ToBoolean(mySqlDataReader["featured"]),
                    //Launchess = lch,
                    //Eventss = even
                };
               
              
                await mySqlConnection.CloseAsync();
               
            }
            return articles;

        }

        public Task Remover(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Launches>> Obter(int idArticles)
        {
            var articless = new List<Articles>();
            var comando = $"select * from articles order by id  limit {quantidade} offset {((pagina - 1) * quantidade)}";
            await mySqlConnection.OpenAsync();
            MySqlCommand mySqlCommand = new MySqlCommand(comando, mySqlConnection);
            MySqlDataReader mySqlDataReader = (MySqlDataReader)await mySqlCommand.ExecuteReaderAsync();
            while (mySqlDataReader.Read())
            {
                articless.Add(new Articles
                {
                    Id = (int)mySqlDataReader["id"],
                    Title = (string)mySqlDataReader["title"],
                    Url = (string)mySqlDataReader["url"],
                    ImageUrl = (string)mySqlDataReader["imageUrl"],
                    NewsSite = (string)mySqlDataReader["newsSite"],
                    Summary = (string)mySqlDataReader["url"],
                    PublishedAt = Convert.ToDateTime(mySqlDataReader["publishedAt"]),
                    UpdateAt = Convert.ToDateTime(mySqlDataReader["updatedAt"]),
                    Featured = Convert.ToBoolean(mySqlDataReader["featured"]),

                });
            }
            await mySqlConnection.CloseAsync();

            return articless;
        }
    }
}
