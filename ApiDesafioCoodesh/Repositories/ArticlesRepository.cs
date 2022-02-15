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
            //mySqlConnection = new MySqlConnection("mysql://y04qwys3d1pfe3ai:giw8ybslfeqf3u9o@yjo6uubt3u5c16az.cbetxkdyhwsb.us-east-1.rds.amazonaws.com:3306/lckckfanuue501ed");
            mySqlConnection = new MySqlConnection(@"server=yjo6uubt3u5c16az.cbetxkdyhwsb.us-east-1.rds.amazonaws.com;database=lckckfanuue501ed;uid=y04qwys3d1pfe3ai;pwd=giw8ybslfeqf3u9o;convert zero datetime=True");
        }
        public async Task Atualizar(Articles articles)
        {
           var comando = $"update articles set  title = '{articles.Title}', url = '{articles.Url}', imageUrl = '{articles.ImageUrl}'," +
                $" newsSite = '{articles.NewsSite}', summary = '{articles.Summary}', publishedAt = '{articles.PublishedAt}'," +
                $" featured = '{articles.Featured}', launches_fk = '{articles.LaunchesProp.Id}', events_fk = '{articles.EventsProp.Id}' where id = '{articles.Id}'";
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
            var comando = "insert into articles (id, title, url, imageUrl, newsSite, summary, publishedAt, featured, launches_fk, events_fk)" +
                $" values ('{articles.Id}', '{articles.Title}', '{articles.Url}', '{articles.ImageUrl}', '{articles.NewsSite}', '{articles.Summary}', '{articles.PublishedAt}'," +
                $" '{articles.Featured}', '{articles.LaunchesProp.Id}', '{articles.EventsProp.Id}')";
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
                    LaunchesProp = new Launches { Id = Convert.ToString(mySqlDataReader["launches_fk"]) },
                    EventsProp = new Events { Id = Convert.ToString(mySqlDataReader["events_fk"]) }

                });
            }
            await mySqlConnection.CloseAsync();
            if (articless == null)
            {
                return null;
            }
            foreach (var art in articless)
            {
                if (!string.IsNullOrEmpty(art.LaunchesProp.Id))
                {
                    var launchesComando = $"select * from launches where id = '{art.LaunchesProp.Id}'";
                    await mySqlConnection.OpenAsync();
                    MySqlCommand launchesMySqlCommand = new MySqlCommand(launchesComando, mySqlConnection);
                    MySqlDataReader launchesMySqlDataReader = (MySqlDataReader)await launchesMySqlCommand.ExecuteReaderAsync();
                    launchesMySqlDataReader.Read();
                    art.LaunchesProp.Provider = Convert.ToString(launchesMySqlDataReader["provider"]);
                    await mySqlConnection.CloseAsync(); 
                }

            }
            foreach (var art in articless)
            {
                if (!string.IsNullOrEmpty(art.EventsProp.Id))
                {
                    var eventsComando = $"select * from events where id = '{art.EventsProp.Id}'";
                    await mySqlConnection.OpenAsync();
                    MySqlCommand eventsMySqlCommand = new MySqlCommand(eventsComando, mySqlConnection);
                    MySqlDataReader eventsMySqlDataReader = (MySqlDataReader)await eventsMySqlCommand.ExecuteReaderAsync();
                    eventsMySqlDataReader.Read();
                    art.EventsProp.Provider = Convert.ToString(eventsMySqlDataReader["provider"]);
                    await mySqlConnection.CloseAsync();
                }

            }
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
                    LaunchesProp = new Launches { Id = Convert.ToString(mySqlDataReader["launches_fk"]) },
                    EventsProp = new Events { Id = Convert.ToString(mySqlDataReader["events_fk"]) }

                };
                await mySqlConnection.CloseAsync();
                if (articles == null)
                {
                    return null;
                }
                if (!string.IsNullOrEmpty(articles.LaunchesProp.Id))
                {
                    var launchesComando = $"select * from launches where id = '{articles.LaunchesProp.Id}'";
                    await mySqlConnection.OpenAsync();
                    MySqlCommand launchesMySqlCommand = new MySqlCommand(launchesComando, mySqlConnection);
                    MySqlDataReader launchesMySqlDataReader = (MySqlDataReader)await launchesMySqlCommand.ExecuteReaderAsync();
                    launchesMySqlDataReader.Read();
                    articles.LaunchesProp.Provider = Convert.ToString(launchesMySqlDataReader["provider"]);
                    await mySqlConnection.CloseAsync();
                }
                if (!string.IsNullOrEmpty(articles.EventsProp.Id))
                {
                    var eventsComando = $"select * from events where id = '{articles.EventsProp.Id}'";
                    await mySqlConnection.OpenAsync();
                    MySqlCommand eventsMySqlCommand = new MySqlCommand(eventsComando, mySqlConnection);
                    MySqlDataReader eventsMySqlDataReader = (MySqlDataReader)await eventsMySqlCommand.ExecuteReaderAsync();
                    eventsMySqlDataReader.Read();
                    articles.EventsProp.Provider = Convert.ToString(eventsMySqlDataReader["provider"]);
                    await mySqlConnection.CloseAsync();
                }
            }
            return articles;

        }

        public Task Remover(int id)
        {
            throw new NotImplementedException();
        }
    }
}
