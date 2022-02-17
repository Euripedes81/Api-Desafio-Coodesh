using HttpClientDesafioCoodesh.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpClientDesafioCoodesh.Repositories
{
    public interface IArticleRepository
    {
       void Inserir(Article article);
        
    }
}
