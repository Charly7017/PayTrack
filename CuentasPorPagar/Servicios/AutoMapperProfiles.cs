using AutoMapper;
using CuentasPorPagar.Models;

namespace CuentasPorPagar.Servicios
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Compra, CompraCreacionViewModel>();
            CreateMap<ControlCalidad, ControlCalidadCreacionViewModel>();
            CreateMap<Gasto, GastoCreacionViewModel>();
        }
    }
}
