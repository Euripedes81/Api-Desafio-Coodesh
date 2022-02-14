using ApiDesafioCoodesh.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiDesafioCoodesh.Controllers.V1
{
    [Route("api/V1[controller]")]
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
    }
}
