using ApiDesafioCoodesh.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiDesafioCoodesh.Repositories
{
    public interface IArticleRepository : IDisposable
    {
        Task<List<Article>> Obter(int pagina, int quantidade);
        Task<Article> Obter(int id);
        Task Inserir(Article articles);
        Task Atualizar(Article articles);
        Task Remover(int id);
    }
}
