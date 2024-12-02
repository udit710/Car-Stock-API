using Dapper;
using Microsoft.Data.Sqlite;

namespace CarStockAPI.Data
{
    public class DbContext
    {
        private readonly string _connectionString;

        public DbContext(IConfiguration configuration)
        {
            // You can configure the database file path via appsettings.json or use a default path
            _connectionString = configuration.GetConnectionString("DefaultConnection") ?? "Data Source=cars.db";
        }

        public SqliteConnection CreateConnection()
        {
            return new SqliteConnection(_connectionString);
        }

        public void Initialize()
        {
            using var connection = CreateConnection();
            connection.Open();

            // Create the Dealers table
            connection.Execute(@"
                CREATE TABLE IF NOT EXISTS Dealers (
                    DealerId INTEGER PRIMARY KEY AUTOINCREMENT,
                    Username TEXT NOT NULL UNIQUE,
                    Password TEXT NOT NULL
                );
            ");

            // Create the Cars table
            connection.Execute(@"
                CREATE TABLE IF NOT EXISTS Cars (
                    CarId INTEGER PRIMARY KEY AUTOINCREMENT,
                    DealerId INTEGER NOT NULL,
                    Make INTEGER NOT NULL,
                    Model TEXT NOT NULL,
                    StockCount INTEGER NOT NULL,
                    FOREIGN KEY (DealerId) REFERENCES Dealers(DealerId) ON DELETE CASCADE
                );
            ");
        }
    }
}
