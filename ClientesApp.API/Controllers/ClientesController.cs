using ClientesApp.API.Entities;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace ClientesApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private string connectionString = "Server=localhost,1433;Database=DBCliente;User Id=sa;Password=SuaSenhaForte@123;TrustServerCertificate=True;Encrypt=False";

        [HttpPost]
        public IActionResult Post([FromForm] string nome, [FromForm] string email)
        {
            var cliente = new Cliente();
            cliente.Nome = nome;
            cliente.Email = email;

            using(var connection = new SqlConnection(connectionString))
            {
                connection.Execute("INSERT INTO CLIENTES(ID, NOME, EMAIL, DATAHORACADASTRO) VALUES(@Id, @Nome, @Email, @DataHoraCadastro)", cliente);
            }

            return Ok("Cliente cadastrado com sucesso!");
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromForm] string nome, [FromForm]string email)
        {
            var cliente = new Cliente();
            cliente.Id = id;
            cliente.Nome = nome;
            cliente.Email = email;

            using(var connection = new SqlConnection(connectionString))
            {
                connection.Execute("UPDATE CLIENTES SET NOME = @Nome, EMAIL = @Email WHERE ID = @Id", cliente);
            }

            return Ok("Cliente atualizado com sucesso.");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            using(var connection = new SqlConnection(connectionString))
            {
                connection.Execute("DELETE FROM CLIENTES WHERE ID = @Id", new { id });
            }

            return Ok("Cliente excluído com sucesso.");
        }

        [HttpGet]
        public IActionResult Get()
        {
            using(var connection = new SqlConnection(connectionString))
            {
                var clientes = connection.Query<Cliente>("SELECT * FROM CLIENTES")
                .ToList();
                return Ok(clientes);
            }
        }
    }
}