using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using TempoControl.Domain;

namespace TempoControl.Infrastructure
{
    public class EmpleadoRepository
    {
        private readonly DbContext _context;

        public EmpleadoRepository()
        {
            _context = new DbContext();
        }

        public void Crear(Empleado empleado)
        {
            using var connection = _context.GetConnection();
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = @"
                INSERT INTO Empleados (NombreCompleto, Departamento, Posicion, Activo)
                VALUES ($nombre, $departamento, $posicion, $activo);
            ";

            command.Parameters.AddWithValue("$nombre", empleado.NombreCompleto);
            command.Parameters.AddWithValue("$departamento", empleado.Departamento);
            command.Parameters.AddWithValue("$posicion", empleado.Posicion);
            command.Parameters.AddWithValue("$activo", empleado.Activo ? 1 : 0);

            command.ExecuteNonQuery();
        }

        public List<Empleado> ObtenerTodos()
        {
            var lista = new List<Empleado>();

            using var connection = _context.GetConnection();
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Empleados";

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                lista.Add(new Empleado
                {
                    Id = reader.GetInt32(0),
                    NombreCompleto = reader.GetString(1),
                    Departamento = reader.GetString(2),
                    Posicion = reader.GetString(3),
                    Activo = reader.GetInt32(4) == 1
                });
            }

            return lista;
        }

        public void Desactivar(int id)
        {
            using var connection = _context.GetConnection();
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = @"
                UPDATE Empleados SET Activo = 0 WHERE Id = $id
            ";

            command.Parameters.AddWithValue("$id", id);

            command.ExecuteNonQuery();
        }
    }
}