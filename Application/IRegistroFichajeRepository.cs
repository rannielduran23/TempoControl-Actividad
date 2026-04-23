using System.Collections.Generic;
using TempoControl.Domain;

namespace TempoControl.Application
{
    public interface IRegistroFichajeRepository
    {
        void RegistrarEntrada(int empleadoId);
        void RegistrarSalida(int empleadoId);
        List<RegistroFichaje> ObtenerPorEmpleado(int empleadoId);
    }
}