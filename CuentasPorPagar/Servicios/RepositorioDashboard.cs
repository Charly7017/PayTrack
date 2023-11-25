using CuentasPorPagar.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CuentasPorPagar.Servicios
{
    public interface IRepositorioDashboard
    {
        Task<IEnumerable<CompraAnualTotal>> ObtenerComprasAnuales();
    }
    public class RepositorioDashboard:IRepositorioDashboard
    {
        private readonly string connectionString;

        public RepositorioDashboard(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<CompraAnualTotal>> ObtenerComprasAnuales()
        {
            using var connection = new SqlConnection(connectionString);

            return await connection.QueryAsync<CompraAnualTotal>("Compra_ObtenerComprasAnuales", commandType: CommandType.StoredProcedure);

        }


    }
}
