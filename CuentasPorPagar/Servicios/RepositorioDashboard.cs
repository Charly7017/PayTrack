using CuentasPorPagar.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CuentasPorPagar.Servicios
{
    public interface IRepositorioDashboard
    {
        Task<IEnumerable<CompraAnualTotal>> ObtenerComprasAnuales();
        Task<IEnumerable<CompraMensualTotal>> ObtenerComprasMensuales();
        Task<IEnumerable<GastoAnualTotal>> ObtenerGastosAnuales();
        Task<IEnumerable<CompraObtenerMes>> ObtenerMeses();
        Task<IEnumerable<VentaAnualTotal>> ObtenerVentasAnuales();
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

        public async Task<IEnumerable<CompraMensualTotal>> ObtenerComprasMensuales()
        {
            using var connection = new SqlConnection(connectionString);

            return await connection.QueryAsync<CompraMensualTotal>("Compra_ObtenerComprasMensuales", commandType: CommandType.StoredProcedure);
        }


        public async Task<IEnumerable<GastoAnualTotal>> ObtenerGastosAnuales()
        {
            using var connection = new SqlConnection(connectionString);

            return await connection.QueryAsync<GastoAnualTotal>("Gasto_ObtenerGastosAnuales", commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<VentaAnualTotal>> ObtenerVentasAnuales()
        {
            using var connection = new SqlConnection(connectionString);

            return await connection.QueryAsync<VentaAnualTotal>("Venta_ObtenerVentasAnuales", commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<CompraObtenerMes>> ObtenerMeses()
        {
            using var connection = new SqlConnection(connectionString);

            return await connection.QueryAsync<CompraObtenerMes>("Compra_ObtenerMeses", commandType: CommandType.StoredProcedure);

        }


    }
}
