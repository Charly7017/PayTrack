using CuentasPorPagar.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CuentasPorPagar.Servicios
{
    public interface IRepositorioMetodoPago
    {
        Task<IEnumerable<MetodoPago>> Obtener();
    }
    public class RepositorioMetodoPago: IRepositorioMetodoPago
    {

        private readonly string connectionString;

        public RepositorioMetodoPago(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }


        public async Task<IEnumerable<MetodoPago>> Obtener()
        {
            using var connection = new SqlConnection(connectionString);

           return await connection.QueryAsync<MetodoPago>("MetodoPago_Obtener",commandType: CommandType.StoredProcedure);

        }



    }
}
