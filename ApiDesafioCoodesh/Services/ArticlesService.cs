using ApiDesafioCoodesh.Entities;
using ApiDesafioCoodesh.InputModel;
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
        public Task Atualizar(ArticlesInputModel articlesInputModel)
        {
            throw new NotImplementedException();
        }

        public async Task<ArticlesViewModel> Inserir(ArticlesInputModel articlesInputModel)
        {
            var articlesInsert = new Articles
            {
                Id = articlesInputModel.Id,
                Title = articlesInputModel.Title,
                Url = articlesInputModel.Url,
                ImageUrl = articlesInputModel?.ImageUrl,
                NewsSite = articlesInputModel.NewsSite,
                Summary = articlesInputModel.Summary,
                PublishedAt = articlesInputModel.PublishedAt,
                Featured = articlesInputModel.Featured,
                LaunchesProp = new Launches { Id = articlesInputModel.LaunchesProp.Id, Provider = articlesInputModel.LaunchesProp.Provider },
                EventsProp = new Events { Id = articlesInputModel.EventsProp.Id, Provider=articlesInputModel.EventsProp.Provider }
            };
            
            await _articlesRepository.Inserir(articlesInsert);
            
            return new ArticlesViewModel
            {
                Id = articlesInsert.Id,
                Title = articlesInsert.Title,
                Url = articlesInsert?.Url,
                ImageUrl = articlesInsert?.ImageUrl,
                NewsSite = articlesInsert.NewsSite,
                Summary = articlesInsert.Summary,
                PublishedAt= articlesInsert.PublishedAt,
                UpdateAt = articlesInsert.UpdateAt,
                Featured = articlesInsert.Featured,
                LaunchesProp = articlesInsert.LaunchesProp,
                EventsProp= articlesInsert.EventsProp
            };
        }


        //public void Dispose()
        //{
        //    throw new NotImplementedException();
        //}


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
                LaunchesProp = articles.LaunchesProp,
                EventsProp = articles.EventsProp

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
                LaunchesProp = articles.LaunchesProp,
                EventsProp = articles.EventsProp
            };
        }

        public Task Remover(int id)
        {
            throw new NotImplementedException();
        }

       
    }
}
