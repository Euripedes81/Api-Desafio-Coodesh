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
    [Route("api/V1/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly IArticlesService _articlesService;

        public ArticlesController(IArticlesService articlesService)
        {
            _articlesService = articlesService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery, Range(1, int.MaxValue)] int pagina = 1, [FromQuery, Range(1, 50)] int quantidade = 5)
        {
            var articless = await _articlesService.Obter(pagina, quantidade);
            if (articless.Count() == 0)
                return NoContent();
            return Ok(articless);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ArticlesViewModel>> GetId([FromRoute] int id)
        {
            var articlesViewModel = await _articlesService.Obter(id);
            if (articlesViewModel == null)
            {
                return null;
            }
            return Ok(articlesViewModel);
        }

        [HttpPost]
        public async Task<ActionResult<ArticlesViewModel>> Post([FromBody] ArticlesInputModel articlesInputModel)
        {
            var articles = await _articlesService.Inserir(articlesInputModel);
            return base.Ok((object)articles);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put ([FromRoute] int id, [FromBody] ArticlesInputModel articlesInputModel)
        {
            try
            {
                await _articlesService.Atualizar(id, articlesInputModel);
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
                await _articlesService.Remover(id);
                return Ok();
            }
            catch (Exception)
            {

                return NotFound("Articles not found!");
            }
        }
    }
}

