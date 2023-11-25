using CuentasPorPagar.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CuentasPorPagar.Servicios
{
    public interface IRepositorioVentas
    {
        Task Actualizar(Venta venta);
        Task Crear(Venta venta);
        Task Eliminar(int id);
        Task<IEnumerable<VentaCreacionViewModel>> Obtener(int usuarioId);
        Task<decimal> ObtenerMontoTotal();
        Task<Venta> ObtenerPorId(int id, int usuarioId);
    }

    public class RepositorioVentas: IRepositorioVentas
    {

        private readonly string connectionString;

        public RepositorioVentas(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task Crear(Venta venta)
        {
            using var connection = new SqlConnection(connectionString);

            var parameters = new
            {
                venta.Fecha,
                venta.Monto,
                venta.UsuarioId,
                venta.Descripcion
            };


            var id = await connection.QuerySingleAsync<int>("Venta_Insertar", parameters, commandType: CommandType.StoredProcedure);

            venta.Id = id;

        }

        public async Task<IEnumerable<VentaCreacionViewModel>> Obtener(int usuarioId)
        {
            using var connection = new SqlConnection(connectionString);

            return await connection.QueryAsync<VentaCreacionViewModel>("Venta_Obtener", new { usuarioId }, commandType: CommandType.StoredProcedure);
        }


        public async Task<Venta> ObtenerPorId(int id, int usuarioId)
        {
            using var connection = new SqlConnection(connectionString);

            var parameters = new
            {
                id,
                usuarioId
            };

            return await connection.QuerySingleOrDefaultAsync<Venta>("Venta_ObtenerPorId", parameters, commandType: CommandType.StoredProcedure);
        }


        public async Task Actualizar(Venta venta)
        {
            using var connection = new SqlConnection(connectionString);

            var parameters = new
            {
                venta.Id,
                venta.Fecha,
                venta.Monto,
                venta.Descripcion
            };

            await connection.ExecuteAsync("Venta_Actualizar", parameters, commandType: CommandType.StoredProcedure);
        }



        public async Task Eliminar(int id)
        {
            using var connection = new SqlConnection(connectionString);

            await connection.ExecuteAsync("Venta_Eliminar", new { id }, commandType: CommandType.StoredProcedure);
        }


        public async Task<decimal> ObtenerMontoTotal()
        {
            using var connection = new SqlConnection(connectionString);

            return await connection.ExecuteScalarAsync<decimal>("Venta_ObtenerMontoTotal", commandType: CommandType.StoredProcedure);
        }



    }
}
