using ApiDesafioCoodesh.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiDesafioCoodesh.Repositories
{
    public interface IArticlesRepository //: IDisposable
    {
        Task<List<Articles>> Obter(int pagina, int quantidade);
        Task<Articles> Obter(int id);
        Task Inserir(Articles articles);
        Task Atualizar(Articles articles);
        Task Remover(int id);
    }
}
