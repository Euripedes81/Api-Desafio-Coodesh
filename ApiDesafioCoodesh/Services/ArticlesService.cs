﻿using ApiDesafioCoodesh.Entities;
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
        public async Task Atualizar(int id, ArticlesInputModel articlesInputModel)
        {
            var articles = await _articlesRepository.Obter(id);
            if (articles == null)
            {
                throw new Exception("Articles not found!");
            }
            else
            {
                articles.Title = articlesInputModel.Title;
                articles.Url = articlesInputModel.Url;
                articles.ImageUrl = articlesInputModel?.ImageUrl;
                articles.NewsSite = articlesInputModel.NewsSite;
                articles.Summary = articlesInputModel.Summary;
                articles.PublishedAt = articlesInputModel.PublishedAt;
                articles.Featured = articlesInputModel.Featured;                
                await _articlesRepository.Atualizar(articles);
            }
        }

        public async Task<ArticlesViewModel> Inserir(ArticlesInputModel articlesInputModel)
        {
            var articlesInsert = new Articles
            {
                Title = articlesInputModel.Title,
                Url = articlesInputModel.Url,
                ImageUrl = articlesInputModel?.ImageUrl,
                NewsSite = articlesInputModel.NewsSite,
                Summary = articlesInputModel.Summary,
                PublishedAt = articlesInputModel.PublishedAt,               
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
                Featured = articlesInsert.Featured
                
            };
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
                Launches = articles.Launchess

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
                Launches = articles.Launchess,
                Events = articles.Eventss
            };
        }

        public async Task Remover(int id)
        {
            var articles = await _articlesRepository.Obter(id);
            if(articles == null)
            {
                throw new Exception("Articles not found!");
            }

            await _articlesRepository.Remover(id);
        }

        //public void Dispose()
        //{
        //    throw new NotImplementedException();
        //}

    }
}
