using CuentasPorPagar.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CuentasPorPagar.Servicios
{
    public interface IRepositorioCompras
    {
        Task Actualizar(Compra compra);
        Task Crear(Compra compra);
        Task Eliminar(int id);
        Task<Compra> ObtenerPorId(int id, int usuarioId);
        Task<bool> Existe(string nombre, int usuarioId);
        Task<IEnumerable<Compra>> Obtener(int usuarioId);
    }
    public class RepositorioCompras:IRepositorioCompras
    {
        private readonly string connectionString;

        public RepositorioCompras(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task Crear(Compra compra)
        {
            using var connection = new SqlConnection(connectionString);


            var parameters = new
            {
                compra.ProveedorId,
                compra.Descripcion,
                compra.FechaCompra,
                compra.Total,
                compra.TipoCompra,
                compra.UsuarioId,
            };


            var id = await connection.QuerySingleAsync<int>("Compra_Insertar", parameters, commandType: CommandType.StoredProcedure);


            compra.Id= id;


        }

        public async Task<IEnumerable<Compra>> Obtener(int usuarioId)
        {
            using var connection = new SqlConnection(connectionString);

            return await connection.QueryAsync<Compra>("Compra_Obtener", new { usuarioId }, commandType: CommandType.StoredProcedure);
        }

        //rama primaris

        public async Task<Compra> ObtenerPorId(int id, int usuarioId)
        {
            using var connection = new SqlConnection(connectionString);

            var parameters = new
            {
                id,
                usuarioId
            };

            return await connection.QuerySingleOrDefaultAsync<Compra>("Compra_ObtenerPorID", parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task Actualizar(Compra compra)
        {
            using var connection = new SqlConnection(connectionString);

            var parameters = new
            {
                compra.Id,
                compra.ProveedorId,
                compra.Descripcion,
                compra.FechaCompra,
                compra.Total,
                compra.TipoCompra
            };


            await connection.ExecuteAsync("Compra_Actualizar", parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task Eliminar(int id)
        {
            using var connection = new SqlConnection(connectionString);


            await connection.ExecuteAsync("Compra_Eliminar", new { id }, commandType: CommandType.StoredProcedure);
        }


        public async Task<bool> Existe(string nombre, int usuarioId)
        {
            using var connection = new SqlConnection(connectionString);

            var parameters = new
            {
                nombre,
                usuarioId
            };

            var existe = await connection.QueryFirstOrDefaultAsync<int>("Compra_Existe",
                parameters, commandType: CommandType.StoredProcedure);

            return existe == 1;
        }


    }
}
