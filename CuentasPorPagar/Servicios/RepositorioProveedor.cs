using CuentasPorPagar.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CuentasPorPagar.Servicios
{
	public interface IRepositorioProveedor
	{
		Task Actualizar(Proveedor proveedor);
		Task Crear(Proveedor proveedor);
		Task Eliminar(int id);
		Task<Proveedor> ObtenerPorId(int id, int usuarioId);
		Task<bool> Existe(string nombre, int usuarioId);
        Task<IEnumerable<Proveedor>> Obtener(int usuarioId);
    }
	public class RepositorioProveedor:IRepositorioProveedor
	{
		private readonly string connectionString;

        public RepositorioProveedor(IConfiguration configuration)
        {
			connectionString = configuration.GetConnectionString("DefaultConnection");
		}

		public async Task Crear(Proveedor proveedor)
		{
			using var connection = new SqlConnection(connectionString);

			var parameters = new
			{
				proveedor.Nombre,
				proveedor.Email,
				proveedor.Direccion,
				proveedor.Telefono,
				proveedor.UsuarioId
			};

			var id = await connection.QuerySingleAsync<int>("Proveedor_Insertar",parameters,commandType:CommandType.StoredProcedure);

			proveedor.Id = id;

		}

		public async Task<IEnumerable<Proveedor>> Obtener(int usuarioId)
		{
            using var connection = new SqlConnection(connectionString);

            return await connection.QueryAsync<Proveedor>("Proveedor_Obtener", new { usuarioId }, commandType: CommandType.StoredProcedure);
        }


        public async Task<Proveedor> ObtenerPorId(int id,int usuarioId)
		{
			using var connection = new SqlConnection(connectionString);

			var parameters = new
			{
				id,
				usuarioId
			};

			return await connection.QuerySingleOrDefaultAsync<Proveedor>("Proveedor_ObtenerPorID", parameters, commandType: CommandType.StoredProcedure);
		}

		public async Task Actualizar(Proveedor proveedor)
		{
			using var connection = new SqlConnection(connectionString);
			var parameters = new
			{
				proveedor.Id,
				proveedor.Nombre,
				proveedor.Email,
				proveedor.Direccion,
				proveedor.Telefono
			};

			await connection.ExecuteAsync("Proveedor_Actualizar", parameters, commandType: CommandType.StoredProcedure);
		}

		public async Task Eliminar(int id)
		{
			using var connection = new SqlConnection(connectionString);
			

			await connection.ExecuteAsync("Proveedor_Eliminar", new {id}, commandType: CommandType.StoredProcedure);
		}

		public async Task<bool> Existe(string nombre, int usuarioId)
		{
			using var connection = new SqlConnection(connectionString);

			var parameters = new
			{
				nombre,
				usuarioId
			};

			var existe = await connection.QueryFirstOrDefaultAsync<int>("Proveedor_Existe",
				parameters, commandType: CommandType.StoredProcedure);

			return existe == 1;
		}
	}
}
