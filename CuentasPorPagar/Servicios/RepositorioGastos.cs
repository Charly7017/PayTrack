using CuentasPorPagar.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CuentasPorPagar.Servicios
{
    public interface IRepositorioGastos
    {
        Task Actualizar(Gasto gasto);
        Task Crear(Gasto gasto);
        Task Eliminar(int id);
        Task<IEnumerable<Gasto>> Obtener(int usuarioId);
        Task<Gasto> ObtenerPorId(int id, int usuarioId);
    }
    public class RepositorioGastos:IRepositorioGastos
    {
        private readonly string connectionString;

        public RepositorioGastos(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task Crear(Gasto gasto)
        {
            using var connection = new SqlConnection(connectionString);

            var parameters = new
            {
                gasto.Descripcion,
                gasto.Fecha,
                gasto.Monto,
                gasto.ProveedorId,
                gasto.MetodoPagoId,
                gasto.UsuarioId
            };

            var id = await connection.QuerySingleAsync<int>("Gasto_Insertar", parameters, commandType: CommandType.StoredProcedure);

            gasto.Id= id;


        }



        public async Task<IEnumerable<Gasto>> Obtener(int usuarioId)
        {
            using var connection = new SqlConnection(connectionString);

            return await connection.QueryAsync<Gasto>("Gasto_Obtener", new { usuarioId }, commandType: CommandType.StoredProcedure);
        }

        public async Task<Gasto> ObtenerPorId(int id, int usuarioId)
        {
            using var connection = new SqlConnection(connectionString);

            var parameters = new
            {
                id,
                usuarioId
            };

            return await connection.QuerySingleOrDefaultAsync<Gasto>("Gasto_ObtenerPorId", parameters, commandType: CommandType.StoredProcedure);
        }




        public async Task Actualizar(Gasto gasto)
        {
            using var connection = new SqlConnection(connectionString);

            var parameters = new
            {
                gasto.Id,
                gasto.Descripcion,
                gasto.Fecha,
                gasto.Monto,
                gasto.ProveedorId,
                gasto.MetodoPagoId,

            };

            await connection.ExecuteAsync("Gasto_Actualizar", parameters, commandType: CommandType.StoredProcedure);
        }


        public async Task Eliminar(int id)
        {
            using var connection = new SqlConnection(connectionString);

            await connection.ExecuteAsync("Gasto_Eliminar", new { id }, commandType: CommandType.StoredProcedure);
        }





    }
}
