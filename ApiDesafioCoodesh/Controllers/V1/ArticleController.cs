using ApiDesafioCoodesh.InputModel;
using ApiDesafioCoodesh.Services;
using ApiDesafioCoodesh.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiDesafioCoodesh.Controllers.V1
{
    [Route("[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleService _articleService;

        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }


        [HttpGet]
        public async Task<IActionResult> Get([FromQuery, Range(1, int.MaxValue)] int inicio = 1, [FromQuery, Range(1, 50)] int quantidade = 5)
        {
            var articles = await _articleService.Obter(inicio, quantidade);
            if (articles.Count() == 0)
                return NoContent();
            return Ok(articles);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ArticleViewModel>> GetId([FromRoute] int id)
        {
            var articlesViewModel = await _articleService.Obter(id);
            if (articlesViewModel == null)
            {
                return NotFound("Article not found!");
            }
            return Ok(articlesViewModel);
        }

        [HttpPost]
        public async Task<ActionResult<ArticleViewModel>> Post([FromBody] ArticleInputModel articlesInputModel)
        {
            var articles = await _articleService.Inserir(articlesInputModel);
            return base.Ok((object)articles);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put ([FromRoute] int id, [FromBody] ArticleInputModel articlesInputModel)
        {
            try
            {
                await _articleService.Atualizar(id, articlesInputModel);
                return Ok();
            }
            catch (Exception)
            {
                return NotFound("Articles not found!");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete ([FromRoute] int id)
        {
            try
            {
                await _articleService.Remover(id);
                return Ok();
            }
            catch (Exception)
            {

                return NotFound("Article not found!");
            }
        }
    }
}

