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
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _articlesRepository;

        public ArticleService(IArticleRepository articlesRepository)
        {
            _articlesRepository = articlesRepository;
        }
        public async Task Atualizar(int id, ArticleInputModel articlesInputModel)
        {
            var article = await _articlesRepository.Obter(id);
            if (article == null)
            {
                throw new Exception("Articles not found!");
            }
            else
            {
                article.Title = articlesInputModel.Title;
                article.Url = articlesInputModel.Url;
                article.ImageUrl = articlesInputModel?.ImageUrl;
                article.NewsSite = articlesInputModel.NewsSite;
                article.Summary = articlesInputModel.Summary;
                article.PublishedAt = articlesInputModel.PublishedAt;
                article.Featured = articlesInputModel.Featured;                
                await _articlesRepository.Atualizar(article);
            }
        }

        public async Task<ArticleViewModel> Inserir(ArticleInputModel articlesInputModel)
        {
            var articleInsert = new Article
            {
                Title = articlesInputModel.Title,
                Url = articlesInputModel.Url,
                ImageUrl = articlesInputModel?.ImageUrl,
                NewsSite = articlesInputModel.NewsSite,
                Summary = articlesInputModel.Summary,
                PublishedAt = articlesInputModel.PublishedAt,               
            };
            
            await _articlesRepository.Inserir(articleInsert);
            
            return new ArticleViewModel
            {
                Id = articleInsert.Id,
                Title = articleInsert.Title,
                Url = articleInsert?.Url,
                ImageUrl = articleInsert?.ImageUrl,
                NewsSite = articleInsert.NewsSite,
                Summary = articleInsert.Summary,
                PublishedAt= articleInsert.PublishedAt,                
                Featured = articleInsert.Featured
                
            };
        }       

        public async Task<List<ArticleViewModel>> Obter(int pagina, int quantidade)
        {
            var articles = await _articlesRepository.Obter(pagina, quantidade);
            return articles.Select(articles => new ArticleViewModel
            {
                Id = articles.Id,
                Title = articles.Title,
                Url = articles.Url,
                ImageUrl = articles.ImageUrl,
                NewsSite = articles.NewsSite,
                Summary = articles.Summary,
                PublishedAt = articles.PublishedAt,                
                Featured = articles.Featured,
                Launches = articles.Launches,
                Events = articles.Events

            }).ToList();
        }

        public async Task<ArticleViewModel> Obter(int id)
        {
            var article = await _articlesRepository.Obter(id);
            if(article == null)
            {
                return null;
            }
            return new ArticleViewModel
            {
                Id = article.Id,
                Title = article.Title,
                Url = article.Url,
                ImageUrl = article.ImageUrl,
                NewsSite = article.NewsSite,
                Summary = article.Summary,
                PublishedAt = article.PublishedAt,                
                Featured = article.Featured,
                Launches = article.Launches,
                Events = article.Events
            };
        }       

        public async Task Remover(int id)
        {
            var article = await _articlesRepository.Obter(id);
            if(article == null)
            {
                throw new Exception("Article not found!");
            }

            await _articlesRepository.Remover(id);
        }

        public void Dispose()
        {
           _articlesRepository?.Dispose();
        }

    }
}
