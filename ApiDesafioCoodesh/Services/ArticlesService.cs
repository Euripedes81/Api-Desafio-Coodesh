using ApiDesafioCoodesh.Repositories;
using ApiDesafioCoodesh.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiDesafioCoodesh.Services
{
    public class ArticlesService : IArticlesService
    {
        private readonly IArticlesRepository _articlesRepository;

        public ArticlesService(IArticlesRepository articlesRepository)
        {
            _articlesRepository = articlesRepository;
        }
        public Task Atualizar(ArticlesViewModel articlesViewModel)
        {
            throw new NotImplementedException();
        }

        //public void Dispose()
        //{
        //    throw new NotImplementedException();
        //}

        public Task Inserir(ArticlesViewModel articlesViewModel)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ArticlesViewModel>> Obter(int pagina, int quantidade)
        {
            var articless = await _articlesRepository.Obter(pagina, quantidade);
            return articless.Select(articles => new ArticlesViewModel
            {
                Id = articles.Id,
                Title = articles.Title,
                Url = articles.Url,
                ImageUrl = articles.ImageUrl,
                NewsSite = articles.NewsSite,
                Summary = articles.Summary,
                PublishedAt = articles.PublishedAt,
                UpdateAt = articles.UpdateAt,
                Featured = articles.Featured,
                Launches = articles.launches,
                Events = articles.events

            }).ToList();
        }

        public async Task<ArticlesViewModel> Obter(int id)
        {
            var articles = await _articlesRepository.Obter(id);
            if(articles == null)
            {
                return null;
            }
            return new ArticlesViewModel
            {
                Id = articles.Id,
                Title = articles.Title,
                Url = articles.Url,
                ImageUrl = articles.ImageUrl,
                NewsSite = articles.NewsSite,
                Summary = articles.Summary,
                PublishedAt = articles.PublishedAt,
                UpdateAt = articles.UpdateAt,
                Featured = articles.Featured,
                Launches = articles.launches,
                Events = articles.events
            };
        }

        public Task Remover(int id)
        {
            throw new NotImplementedException();
        }
    }
}
