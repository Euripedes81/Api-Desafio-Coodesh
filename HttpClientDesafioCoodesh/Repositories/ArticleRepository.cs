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
        public void Inserir(List<Article> articles)
        {           
            
            mySqlConnection.Open();
            foreach (var article in articles)
            {              

                if (ObterId(article.Id))
                {                   
                    continue;
                }
                
                Console.WriteLine($"Salvando Id:{article.Id} Title: {article.Title}");
               
                var comandoSql = "insert into article (id, title, url, imageUrl, newsSite, summary, publishedAt, updatedAt, featured)" +
                      $" values ('{article.Id}', '{article.Title}', '{article.Url}', '{article.ImageUrl}', '{article.NewsSite}', '{article.Summary}', '{article.PublishedAt}', '{article.UpdateAt}', {article.Featured})";
                               
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
            mySqlConnection.Close();
        }
        private void ExecutarComandoDB(string comando)
        {            
            MySqlCommand mySqlCommand = new MySqlCommand(comando, mySqlConnection);
            mySqlCommand.ExecuteNonQuery();           
        }
        private bool ObterId(int id)
        {
            Article article;
            var comando = $"select * from article where id = '{id}'";           
            MySqlCommand mySqlCommand = new MySqlCommand(comando, mySqlConnection);
            MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
           
            if (mySqlDataReader.Read())
            {
                article = new Article
                {
                    Id = (int)mySqlDataReader["id"]               
                };
                
                if (article.Id == id)
                {
                    mySqlDataReader.Close();
                    return true;
                }
            }
            mySqlDataReader.Close();
            return false;
        }
        public void Dispose()
        {
            mySqlConnection?.Close();
            mySqlConnection?.Dispose();
        }
    }
}
