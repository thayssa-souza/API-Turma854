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

        public bool InsertProduto(Produto produto)
        {
            var query = "INSERT INTO Produtos VALUES (@descricao, @preco, @quantidade)";

            var parameters = new DynamicParameters();
            parameters.Add("descricao", produto.Descricao);
            parameters.Add("preco", produto.Preco);
            parameters.Add("quantidade", produto.Quantidade);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            
            return conn.Execute(query, parameters) == 1;
        }

        public bool DeleteProduto(long id)
        {
            var query = "DELETE FROM Produtos WHERE id = @id";

            var parameters = new DynamicParameters();
            parameters.Add("id", id);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Execute(query, parameters) == 1;
        }

        public bool UpdateProduto(long id, Produto produto)
        {
            var query = "UPDATE Produtos SET descricao = @descricao," +
                "preco = @preco, quantidade = @quantidade WHERE id = @id";

            var parameters = new DynamicParameters();
            parameters.Add("id", produto.Id);
            parameters.Add("descricao", produto.Descricao);
            parameters.Add("preco", produto.Preco);
            parameters.Add("quantidade", produto.Quantidade);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Execute(query, parameters) == 1;
        }

        public List<Produto> SelectProdutos(string descricao)
        {
            var query = "SELECT * FROM Produtos";
            var parameters = new DynamicParameters();
            parameters.Add("descricao", descricao);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Query<Produto>(query).ToList();
        }
    }
}
