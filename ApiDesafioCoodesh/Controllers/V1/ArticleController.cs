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
            var articleViewModel = await _articleService.Obter(id);
            if (articleViewModel == null)
            {
                return NotFound("Article not found!");
            }
            return Ok(articleViewModel);
        }

        [HttpPost]
        public async Task<ActionResult<ArticleViewModel>> Post([FromBody] ArticleInputModel articleInputModel)
        {
            var article = await _articleService.Inserir(articleInputModel);
            return base.Ok((object)article);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put ([FromRoute] int id, [FromBody] ArticleInputModel articleInputModel)
        {
            try
            {
                await _articleService.Atualizar(id, articleInputModel);
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

