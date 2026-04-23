using System.Collections.Generic;
using TempoControl.Domain;

namespace TempoControl.Application
{
    public interface IEmpleadoRepository
    {
        void Crear(Empleado empleado);
        List<Empleado> ObtenerTodos();
        Empleado? ObtenerPorId(int id); // 👈 IMPORTANTE el ?
        void Actualizar(Empleado empleado);
        void Desactivar(int id);
    }
}