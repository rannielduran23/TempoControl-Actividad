using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using TempoControl.Domain;

namespace TempoControl.Infrastructure
{
    public class RegistroFichajeRepository
    {
        private readonly DbContext _context;

        public RegistroFichajeRepository()
        {
            _context = new DbContext();
        }

        public void RegistrarEntrada(int empleadoId)
        {
            using var connection = _context.GetConnection();
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = @"
                INSERT INTO Fichajes (EmpleadoId, HoraEntrada, HoraSalida)
                VALUES ($empleadoId, $entrada, NULL);
            ";

            command.Parameters.AddWithValue("$empleadoId", empleadoId);
            command.Parameters.AddWithValue("$entrada", DateTime.Now.ToString("s"));

            command.ExecuteNonQuery();

            Console.WriteLine("Entrada registrada!");
        }

        public void RegistrarSalida(int empleadoId)
        {
            using var connection = _context.GetConnection();
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = @"
                UPDATE Fichajes
                SET HoraSalida = $salida
                WHERE Id = (
                    SELECT Id FROM Fichajes
                    WHERE EmpleadoId = $empleadoId AND HoraSalida IS NULL
                    ORDER BY Id DESC LIMIT 1
                );
            ";

            command.Parameters.AddWithValue("$empleadoId", empleadoId);
            command.Parameters.AddWithValue("$salida", DateTime.Now.ToString("s"));

            command.ExecuteNonQuery();

            Console.WriteLine("Salida registrada!");
        }

        public List<RegistroFichaje> ObtenerPorEmpleado(int empleadoId)
        {
            var lista = new List<RegistroFichaje>();

            using var connection = _context.GetConnection();
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = @"
                SELECT * FROM Fichajes WHERE EmpleadoId = $empleadoId
            ";

            command.Parameters.AddWithValue("$empleadoId", empleadoId);

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                lista.Add(new RegistroFichaje
                {
                    Id = reader.GetInt32(0),
                    EmpleadoId = reader.GetInt32(1),
                    HoraEntrada = DateTime.Parse(reader.GetString(2)),
                    HoraSalida = reader.IsDBNull(3) ? null : DateTime.Parse(reader.GetString(3))
                });
            }

            return lista;
        }
    }
}