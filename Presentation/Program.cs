using System;
using TempoControl.Infrastructure;
using TempoControl.Domain;
using TempoControl.Application;

namespace TempoControl.Presentation
{
    class Program
    {
        static void Main(string[] args)
        {
            var dbContext = new DbContext();
            var initializer = new DatabaseInitializer(dbContext);
             initializer.Inicializar();
            var repoEmpleado = new EmpleadoRepository();
            var repoFichaje = new RegistroFichajeRepository();
            var reporteService = new ReporteService(repoEmpleado, repoFichaje);

            bool salir = false;

            while (!salir)
            {
                Console.WriteLine("\n-_TEMPOCONTROL_-");
                Console.WriteLine("1. Crear empleado");
                Console.WriteLine("2. Ver empleados");
                Console.WriteLine("3. Desactivar empleado");
                Console.WriteLine("4. Registrar entrada");
                Console.WriteLine("5. Registrar salida");
                Console.WriteLine("6. Ver fichajes de empleado");
                Console.WriteLine("7. Salir");
                Console.WriteLine("8. Reporte mensual");
                Console.Write("Seleccione una opción: ");

                var opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        CrearEmpleado(repoEmpleado);
                        break;

                    case "2":
                        MostrarEmpleados(repoEmpleado);
                        break;

                    case "3":
                        DesactivarEmpleado(repoEmpleado);
                        break;

                    case "4":
                        RegistrarEntrada(repoFichaje);
                        break;

                    case "5":
                        RegistrarSalida(repoFichaje);
                        break;

                    case "6":
                        VerFichajes(repoFichaje);
                        break;

                    case "7":
                        salir = true;
                        break;

                    case "8":
                        Console.Write("Ingrese mes (1-12): ");
                        int mes = int.Parse(Console.ReadLine() ?? "0");

                        Console.Write("Ingrese año: ");
                        int año = int.Parse(Console.ReadLine() ?? "0");

                        reporteService.GenerarReporteMensual(mes, año);
                        break;

                    default:
                        Console.WriteLine("Opción inválida");
                        break;
                }
            }
        }

        static void CrearEmpleado(EmpleadoRepository repo)
        {
            Console.Write("Nombre: ");
            string nombre = Console.ReadLine() ?? "";

            Console.Write("Departamento: ");
            string departamento = Console.ReadLine() ?? "";

            Console.Write("Posición: ");
            string posicion = Console.ReadLine() ?? "";

            var empleado = new Empleado
            {
                NombreCompleto = nombre,
                Departamento = departamento,
                Posicion = posicion
            };

            repo.Crear(empleado);
            Console.WriteLine("Empleado creado correctamente!");
        }

        static void MostrarEmpleados(EmpleadoRepository repo)
        {
            var empleados = repo.ObtenerTodos();

            Console.WriteLine("\n--- Lista de Empleados ---");

            foreach (var e in empleados)
            {
                Console.WriteLine($"ID: {e.Id} | Nombre: {e.NombreCompleto} | Dep: {e.Departamento} | Pos: {e.Posicion} | Activo: {e.Activo}");
            }
        }

        static void DesactivarEmpleado(EmpleadoRepository repo)
        {
            Console.Write("Ingrese ID del empleado: ");
            int id = int.Parse(Console.ReadLine() ?? "0");

            repo.Desactivar(id);
            Console.WriteLine("Empleado desactivado!");
        }

        static void RegistrarEntrada(RegistroFichajeRepository repo)
        {
            Console.Write("ID del empleado: ");
            int id = int.Parse(Console.ReadLine() ?? "0");

            repo.RegistrarEntrada(id);
        }

        static void RegistrarSalida(RegistroFichajeRepository repo)
        {
            Console.Write("ID del empleado: ");
            int id = int.Parse(Console.ReadLine() ?? "0");

            repo.RegistrarSalida(id);
        }

        static void VerFichajes(RegistroFichajeRepository repo)
        {
            Console.Write("ID del empleado: ");
            int id = int.Parse(Console.ReadLine() ?? "0");

            var registros = repo.ObtenerPorEmpleado(id);

            Console.WriteLine("\n--- Fichajes ---");

            foreach (var r in registros)
            {
                Console.WriteLine($"Entrada: {r.HoraEntrada} | Salida: {r.HoraSalida}");
            }
        }
    }
}