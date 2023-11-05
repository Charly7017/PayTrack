using CuentasPorPagar.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CuentasPorPagar.Servicios
{
    public interface IRepositorioControlCalidad
    {
        Task Actualizar(ControlCalidad controlCalidad);
        Task Crear(ControlCalidad controlCalidad);
        Task Eliminar(int id);
        Task<IEnumerable<ControlCalidad>> Obtener(int usuarioId);
        Task<ControlCalidad> ObtenerPorId(int id, int usuarioId);
    }

    public class RepositorioControlCalidad: IRepositorioControlCalidad
	{
		private readonly string connectionString;

		public RepositorioControlCalidad(IConfiguration configuration)
		{
			connectionString = configuration.GetConnectionString("DefaultConnection");
		}

		public async Task Crear(ControlCalidad controlCalidad)
		{
			using var connection = new SqlConnection(connectionString);

			var parameters = new
			{
				controlCalidad.CompraId,
				controlCalidad.Fecha,
				controlCalidad.Estado,
				controlCalidad.UsuarioId,
                controlCalidad.Descripcion
			};

			var id = await connection.QuerySingleAsync<int>("ControlCalidad_Insertar", parameters, commandType: CommandType.StoredProcedure);

			controlCalidad.Id = id;

		}



        public async Task<IEnumerable<ControlCalidad>> Obtener(int usuarioId)
        {
            using var connection = new SqlConnection(connectionString);

            return await connection.QueryAsync<ControlCalidad>("ControlCalidad_Obtener", new { usuarioId }, commandType: CommandType.StoredProcedure);
        }


        public async Task<ControlCalidad> ObtenerPorId(int id, int usuarioId)
        {
            using var connection = new SqlConnection(connectionString);

            var parameters = new
            {
                id,
                usuarioId
            };

            return await connection.QuerySingleOrDefaultAsync<ControlCalidad>("ControlCalidad_ObtenerPorId", parameters, commandType: CommandType.StoredProcedure);
        }




        public async Task Actualizar(ControlCalidad controlCalidad)
        {
            using var connection = new SqlConnection(connectionString);

            var parameters = new
            {
                controlCalidad.Id,
                controlCalidad.CompraId,
                controlCalidad.Fecha,
                controlCalidad.Estado,
                controlCalidad.Descripcion

            };

            await connection.ExecuteAsync("ControlCalidad_Actualizar", parameters, commandType: CommandType.StoredProcedure);
        }


        public async Task Eliminar(int id)
        {
            using var connection = new SqlConnection(connectionString);

            await connection.ExecuteAsync("ControlCalidad_Eliminar", new { id }, commandType: CommandType.StoredProcedure);
        }





    }
}
