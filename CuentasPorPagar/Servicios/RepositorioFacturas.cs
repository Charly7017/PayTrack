namespace CuentasPorPagar.Servicios
{
    public interface IRepositorioFacturas
    {

    }

    public class RepositorioFacturas
    {
        private readonly string connectionString;

        public RepositorioFacturas(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }
    }
}
