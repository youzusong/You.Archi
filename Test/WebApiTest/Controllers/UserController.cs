using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using WebApiTest.Dtos;
using Dapper;

namespace WebApiTest.Controllers
{
    [ApiController]
    [Route("/{controller}/{action}")]
    public class UserController : Controller
    {
        private const string CONN_STR = @"Server=(local)\SQLSVR2016;database=You_Archi;uid=sa;pwd=handcome;Connect Timeout=30;";

        [HttpGet]
        public IReadOnlyList<UserDto> List()
        {
            var cmdText = "SELECT * FROM [User]";
            using (var conn = new SqlConnection(CONN_STR))
            {
                return conn.Query<UserDto>(cmdText).ToList();
            }
        }

        [HttpGet]
        public Guid Add(string name)
        {
            var cmdText = "INSERT INTO [User] VALUES (@Id, @Name)";
            var id = Guid.NewGuid();
            using (var conn = new SqlConnection(CONN_STR))
            {
                conn.Execute(cmdText, new { Id = id, Name = name });
                return id;
            }
        }
    }
}
