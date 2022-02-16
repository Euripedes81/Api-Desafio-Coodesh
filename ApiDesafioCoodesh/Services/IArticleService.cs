using ApiDesafioCoodesh.InputModel;
using ApiDesafioCoodesh.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiDesafioCoodesh.Services
{
    public interface IArticleService : IDisposable
    {
        Task<List<ArticleViewModel>> Obter(int pagina, int quantidade);
        Task<ArticleViewModel> Obter(int id);
        Task<ArticleViewModel> Inserir(ArticleInputModel articlesInputModel);
        Task Atualizar(int id, ArticleInputModel articlesInputModel);
        Task Remover(int id);       
    }
}
