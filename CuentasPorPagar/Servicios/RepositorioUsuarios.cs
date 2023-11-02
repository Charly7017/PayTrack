using CuentasPorPagar.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CuentasPorPagar.Servicios
{
    public interface IRepositorioUsuarios
    {
        Task<Usuario> BuscarUsuarioPorEmail(string emailNormalizado);
        Task<int> CrearUsuario(Usuario usuario);
    }

    public class RepositorioUsuarios : IRepositorioUsuarios
    {
        private readonly string connectionString;
        public RepositorioUsuarios(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<int> CrearUsuario(Usuario usuario)
        {
            using var connection = new SqlConnection(connectionString);

            var parameter = new
            {
                usuario.Email,
                usuario.EmailNormalizado,
                usuario.PasswordHash
            };

            var id = await connection.QuerySingleAsync<int>("Usuario_Insertar", parameter, commandType: CommandType.StoredProcedure);

            //await connection.ExecuteAsync("CrearDatosUsuarioNuevo", new { usuarioId },
            //    commandType: CommandType.StoredProcedure);

            return id;
        }

        public async Task<Usuario> BuscarUsuarioPorEmail(string emailNormalizado)
        {
            using var connection = new SqlConnection(connectionString);
            var usuario = await connection.QuerySingleOrDefaultAsync<Usuario>("Usuario_ObtenerPorEmail",
                new { emailNormalizado });
            return usuario;
        }
    }

}
