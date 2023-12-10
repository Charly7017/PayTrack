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
        Task<IEnumerable<ObtenerAño>> ObtenerAñosCompra();
        Task<IEnumerable<VentaAnualTotal>> ObtenerVentasAnuales();
        Task<IEnumerable<ObtenerAño>> ObtenerAñosVenta();
        Task<IEnumerable<ObtenerAño>> ObtenerAñosGasto();
        Task<IEnumerable<GastoMensualTotal>> ObtenerGastosMensuales();
        Task<IEnumerable<VentaMensualTotal>> ObtenerVentasMensuales();
        //Task<CompraMensualTotal> ObtenerComprasPorAño(int año);
        Task<IEnumerable<CompraMensualTotal>> ObtenerComprasPorAño(int valorSelect);
        Task<IEnumerable<VentaMensualTotal>> ObtenerVentasPorAño(int año);
        Task<IEnumerable<GastoMensualTotal>> ObtenerGastosPorAño(int año);
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

        public async Task<IEnumerable<CompraMensualTotal>> ObtenerComprasPorAño(int año)
        {
            using var connection = new SqlConnection(connectionString);
            


            return await connection.QueryAsync<CompraMensualTotal>("Compra_ObtenerComprasMensualesPorAño",new { año},commandType:CommandType.StoredProcedure);

        }

        public async Task<IEnumerable<GastoAnualTotal>> ObtenerGastosAnuales()
        {
            using var connection = new SqlConnection(connectionString);

            return await connection.QueryAsync<GastoAnualTotal>("Gasto_ObtenerGastosAnuales", commandType: CommandType.StoredProcedure);
        }


        public async Task<IEnumerable<GastoMensualTotal>> ObtenerGastosMensuales()
        {
            using var connection = new SqlConnection(connectionString);

            return await connection.QueryAsync<GastoMensualTotal>("Gasto_ObtenerGastosMensuales",commandType:CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<GastoMensualTotal>> ObtenerGastosPorAño(int año)
        {
            using var connection = new SqlConnection(connectionString);



            return await connection.QueryAsync<GastoMensualTotal>("Gasto_ObtenerGastosMensualesPorAño", new { año }, commandType: CommandType.StoredProcedure);

        }

        public async Task<IEnumerable<VentaAnualTotal>> ObtenerVentasAnuales()
        {
            using var connection = new SqlConnection(connectionString);

            return await connection.QueryAsync<VentaAnualTotal>("Venta_ObtenerVentasAnuales", commandType: CommandType.StoredProcedure);
        }



        public async Task<IEnumerable<VentaMensualTotal>> ObtenerVentasMensuales()
        {
            using var connection = new SqlConnection(connectionString);

            return await connection.QueryAsync<VentaMensualTotal>("Venta_ObtenerVentasMensuales", commandType: CommandType.StoredProcedure);
        }


        public async Task<IEnumerable<VentaMensualTotal>> ObtenerVentasPorAño(int año)
        {
            using var connection = new SqlConnection(connectionString);



            return await connection.QueryAsync<VentaMensualTotal>("Venta_ObtenerVentasMensualesPorAño", new { año }, commandType: CommandType.StoredProcedure);

        }





        public async Task<IEnumerable<ObtenerAño>> ObtenerAñosCompra()
        {
            using var connection = new SqlConnection(connectionString);

            return await connection.QueryAsync<ObtenerAño>("Compra_ObtenerAños", commandType: CommandType.StoredProcedure);

        }

        public async Task<IEnumerable<ObtenerAño>> ObtenerAñosGasto()
        {
            using var connection = new SqlConnection(connectionString);

            return await connection.QueryAsync<ObtenerAño>("Gasto_ObtenerAños", commandType: CommandType.StoredProcedure);

        }

        public async Task<IEnumerable<ObtenerAño>> ObtenerAñosVenta()
        {
            using var connection = new SqlConnection(connectionString);

            return await connection.QueryAsync<ObtenerAño>("Venta_ObtenerAños", commandType: CommandType.StoredProcedure);

        }

  



    }
}
