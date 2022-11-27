using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using PersonalDiary.BLL.Interfaces;

namespace PersonalDiary.BLL.Service
{
    internal class ProceduresService : IProceduresService
    {
        private readonly IConfiguration _configuration;

        public ProceduresService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task DeleteNonRestoredUsers()
        {
            string sqlExpression = "DeleteNonRestoredUsers";

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();

                var command = new SqlCommand(sqlExpression, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                await command.ExecuteNonQueryAsync();
            }
        }
    }
}
