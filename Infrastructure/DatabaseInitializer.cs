using Microsoft.Data.Sqlite;

namespace TempoControl.Infrastructure
{
    public class DatabaseInitializer
    {
        private readonly DbContext _context;

        public DatabaseInitializer(DbContext context)
        {
            _context = context;
        }

        public void Inicializar()
        {
            using var connection = _context.GetConnection();
            connection.Open();

            var command = connection.CreateCommand();

            command.CommandText = @"
            CREATE TABLE IF NOT EXISTS Empleados (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                NombreCompleto TEXT,
                Departamento TEXT,
                Posicion TEXT,
                Activo INTEGER
            );

            CREATE TABLE IF NOT EXISTS Fichajes (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                EmpleadoId INTEGER,
                HoraEntrada TEXT,
                HoraSalida TEXT
            );
            ";

            command.ExecuteNonQuery();
        }
    }
}