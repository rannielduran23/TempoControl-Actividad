using System;
using System.Linq;
using TempoControl.Domain;
using TempoControl.Infrastructure;

namespace TempoControl.Application
{
    public class ReporteService
    {
        private readonly EmpleadoRepository _empleadoRepo;
        private readonly RegistroFichajeRepository _fichajeRepo;

        public ReporteService(EmpleadoRepository empleadoRepo, RegistroFichajeRepository fichajeRepo)
        {
            _empleadoRepo = empleadoRepo;
            _fichajeRepo = fichajeRepo;
        }

        public void GenerarReporteMensual(int mes, int año)
        {
            var empleados = _empleadoRepo.ObtenerTodos();

            Console.WriteLine("\n=== REPORTE MENSUAL ===");

            foreach (var emp in empleados)
            {
                var registros = _fichajeRepo.ObtenerPorEmpleado(emp.Id)
                    .Where(r => r.HoraEntrada.Month == mes &&
                                r.HoraEntrada.Year == año &&
                                r.HoraSalida.HasValue)
                    .ToList();

                int diasTrabajados = registros.Count;

                double totalHoras = 0;

                foreach (var r in registros)
                {
                    if (r.HoraSalida.HasValue)
                    {
                        totalHoras += (r.HoraSalida.Value - r.HoraEntrada).TotalHours;
                    }
                }

                Console.WriteLine($"Empleado: {emp.NombreCompleto}");
                Console.WriteLine($"Días trabajados: {diasTrabajados}");
                Console.WriteLine($"Horas trabajadas: {totalHoras:F2}");
                Console.WriteLine("-----------------------------");
            }
        }
    }
}