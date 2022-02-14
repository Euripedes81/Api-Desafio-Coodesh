using ApiDesafioCoodesh.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiDesafioCoodesh.Services
{
    public interface IArticlesService //: IDisposable
    {
        Task<List<ArticlesViewModel>> Obter(int pagina, int quantidade);
        Task<ArticlesViewModel> Obter(int id);
        Task Inserir(ArticlesViewModel articlesViewModel);
        Task Atualizar(ArticlesViewModel articlesViewModel);
        Task Remover(int id);
    }
}
