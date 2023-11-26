using CuentasPorPagar.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CuentasPorPagar.Servicios
{
    public interface IRepositorioEstadoReporte
    {
        //Task<IEnumerable<EstadoReporte>> ObtenerBeneficioDiario();
        Task<IEnumerable<EstadoReporte>> ObtenerBeneficioDiario(int? year = null);
    }
    public class RepositorioEstadoReporte:IRepositorioEstadoReporte
    {
        private readonly string connectionString;

        public RepositorioEstadoReporte(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }


        public async Task<IEnumerable<EstadoReporte>> ObtenerBeneficioDiario(int? year=null)
        {
            using var connection = new SqlConnection(connectionString);

            var parameters = new DynamicParameters();

            if (year.HasValue)
            {
                parameters.Add("@Year",year.Value,DbType.Int32);
            }


            return await connection.QueryAsync<EstadoReporte>(
                   "CalcularUtilidadDiaria",
                   parameters,
                   commandType: CommandType.StoredProcedure
               );

        }


    }
}
