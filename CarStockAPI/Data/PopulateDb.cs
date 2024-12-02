using Dapper;

namespace CarStockAPI.Data
{
    public class PopulateDb
    {
        private readonly DbContext _dbContext;

        public PopulateDb(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void populate()
        {
            using var connection = _dbContext.CreateConnection();
            connection.Execute("INSERT INTO Dealers (Username, Password) VALUES ('admin', 'admin')");
            connection.Execute("INSERT INTO Dealers (Username, Password) VALUES ('dealers3', 'password1')");
            connection.Execute("INSERT INTO Dealers (Username, Password) VALUES ('dealers4', 'dealer2')");
            connection.Execute("INSERT INTO Cars (DealerId, Make, Model, StockCount) VALUES (2, 1, 'Model 1', 10)");
            connection.Execute("INSERT INTO Cars (DealerId, Make, Model, StockCount) VALUES (2, 2, 'Model 2', 5)");
            connection.Execute("INSERT INTO Cars (DealerId, Make, Model, StockCount) VALUES (3, 1, 'Model 1', 7)");
            connection.Execute("INSERT INTO Cars (DealerId, Make, Model, StockCount) VALUES (3, 2, 'Model 2', 3)");
        }
    }
}