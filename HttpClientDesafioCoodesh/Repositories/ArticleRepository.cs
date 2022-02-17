using HttpClientDesafioCoodesh.Entities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpClientDesafioCoodesh.Repositories
{
    internal class ArticleRepository : IArticleRepository
    {
        private readonly MySqlConnection mySqlConnection = new MySqlConnection(@"server=x8autxobia7sgh74.cbetxkdyhwsb.us-east-1.rds.amazonaws.com;database=of0pkqgq9unpjqua;uid=sbpkruojpnkxzhol;pwd=irdicmhft81rqc3o;convert zero datetime=True");

        public void Inserir(Article article)
        {
            var comandoSql = "insert into article (id, title, url, imageUrl, newsSite, summary, publishedAt, updatedAt, featured)" +
               $" values ('{article.Id}', '{article.Title}', '{article.Url}', '{article.ImageUrl}', '{article.NewsSite}', '{article.Summary}', '{article.PublishedAt}', '{article.UpdateAt}', '{article.Featured}')";
            ExecutarComandoDB(comandoSql);

            foreach (var launch in article.Launches)
            {
                comandoSql = $"insert into launch (id, provider, article_fk ) values ('{launch.Id}', '{launch.Provider}' , '{article.Id}')";
                ExecutarComandoDB(comandoSql);
            }

            foreach (var ev in article.Events)
            {
                comandoSql = $"insert into event (id, provider, article_fk ) values ('{ev.Id}', '{ev.Provider}' , '{article.Id}')";
                ExecutarComandoDB(comandoSql);
            }            
        }
        private void ExecutarComandoDB(string comando)
        {
            mySqlConnection.Open();
            MySqlCommand mySqlCommand = new MySqlCommand(comando, mySqlConnection);
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
        }

        public void Dispose()
        {
            mySqlConnection?.Close();
            mySqlConnection?.Dispose();
        }
    }
}
