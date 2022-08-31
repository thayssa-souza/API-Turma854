using Dapper;
using Microsoft.Data.SqlClient;

namespace APIProdutos.Repository
{
    public class ProdutoRepository
    {
        private readonly IConfiguration _configuration;

        public ProdutoRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<Produto> GetProdutos()
        {
            var query = "SELECT * FROM Produtos";

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            
            return conn.Query<Produto>(query).ToList();
        }
    }
}
