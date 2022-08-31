using APIProdutos.Repository;
using Microsoft.AspNetCore.Mvc;

namespace APIProdutos.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutoController : ControllerBase
    {
        public List<Produto> ProdutoList { get; set; }
        
        private readonly IConfiguration _configuration;

        public ProdutoController(IConfiguration configuration)
        {
            ProdutoList = new List<Produto>();
            _configuration = configuration;
        }

        [HttpGet("/produto/{descricao}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Produto> GetProduto(string descricao)
        {
            var produtos = ProdutoList;
            if (produtos == null)
            {
                return NotFound();
            }
            return Ok(produtos);
        }

        [HttpGet("/produto")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<Produto>> GetProdutos()
        {
            var repositoryProduto = new ProdutoRepository(_configuration);
            var produtos = repositoryProduto.GetProdutos();
            return Ok(produtos);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<List<Produto>> PostProduto(Produto produto)
        {
            ProdutoList.Add(produto);
            return CreatedAtAction(nameof(PostProduto), produto);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateProduto(long id, Produto produto)
        {
            var produtos = ProdutoList;
            if (produtos == null)
                return NotFound();

            return NoContent();
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<Produto>> DeleteProduto(long id)
        {
            var produtos = ProdutoList;
            if (produtos == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}