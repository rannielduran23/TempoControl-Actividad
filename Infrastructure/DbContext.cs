using Microsoft.Data.Sqlite;

namespace TempoControl.Infrastructure
{
    public class DbContext
    {
        private readonly string connectionString = "Data Source=tempo.db";

        public SqliteConnection GetConnection()
        {
            return new SqliteConnection(connectionString);
        }
    }
}