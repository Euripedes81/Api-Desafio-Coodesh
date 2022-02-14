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
            mySqlConnection = new MySqlConnection(@"server=yjo6uubt3u5c16az.cbetxkdyhwsb.us-east-1.rds.amazonaws.com;database=lckckfanuue501ed;uid=y04qwys3d1pfe3ai;pwd=giw8ybslfeqf3u9o");
        }
        public Task Atualizar(Articles articles)
        {
            throw new NotImplementedException();
        }

        //public void Dispose()
        //{
        //    throw new NotImplementedException();
        //}

        public Task Inserir(Articles articles)
        {
            throw new NotImplementedException();
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
                    PublishedAt = (DateTime)mySqlDataReader["publishedAt"],
                    UpdateAt = (DateTime)mySqlDataReader["updatedAt"],
                    Featured = Convert.ToBoolean(mySqlDataReader["featured"]),
                    launches = new Launches { Id = Convert.ToString(mySqlDataReader["launches_fk"]) },
                    events = new Events { Id = Convert.ToString(mySqlDataReader["events_fk"]) }

                });
            }
            await mySqlConnection.CloseAsync();
            if (articless == null)
            {
                return null;
            }
            foreach (var art in articless)
            {
                if (!string.IsNullOrEmpty(art.launches.Id))
                {
                    var launchesComando = $"select * from launches where id = '{art.launches.Id}'";
                    await mySqlConnection.OpenAsync();
                    MySqlCommand launchesMySqlCommand = new MySqlCommand(launchesComando, mySqlConnection);
                    MySqlDataReader launchesMySqlDataReader = (MySqlDataReader)await launchesMySqlCommand.ExecuteReaderAsync();
                    launchesMySqlDataReader.Read();
                    art.launches.Provider = Convert.ToString(launchesMySqlDataReader["provider"]);
                    await mySqlConnection.CloseAsync(); 
                }

            }
            foreach (var art in articless)
            {
                if (!string.IsNullOrEmpty(art.events.Id))
                {
                    var eventsComando = $"select * from events where id = '{art.events.Id}'";
                    await mySqlConnection.OpenAsync();
                    MySqlCommand eventsMySqlCommand = new MySqlCommand(eventsComando, mySqlConnection);
                    MySqlDataReader eventsMySqlDataReader = (MySqlDataReader)await eventsMySqlCommand.ExecuteReaderAsync();
                    eventsMySqlDataReader.Read();
                    art.events.Provider = Convert.ToString(eventsMySqlDataReader["provider"]);
                    await mySqlConnection.CloseAsync();
                }

            }
            return articless;
        }

        public Task<Articles> Obter(int id)
        {
            throw new NotImplementedException();
        }

        public Task Remover(int id)
        {
            throw new NotImplementedException();
        }
    }
}
