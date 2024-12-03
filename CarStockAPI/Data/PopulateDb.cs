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
            connection.Execute(@"
                INSERT INTO Dealers (Username, Password) VALUES ('dealer1', 'password1');
                INSERT INTO Dealers (Username, Password) VALUES ('dealer2', 'password2');
                INSERT INTO Dealers (Username, Password) VALUES ('dealer3', 'password3');
                INSERT INTO Dealers (Username, Password) VALUES ('dealer4', 'password4');
                INSERT INTO Dealers (Username, Password) VALUES ('dealer5', 'password5');
            ");
            connection.Execute(@"
                INSERT INTO Cars (DealerId, Make, Model, Year, StockCount) VALUES (1, 'Toyota', 'Corolla', 2021, 10);
                INSERT INTO Cars (DealerId, Make, Model, Year, StockCount) VALUES (1, 'Toyota', 'Camry', 2021, 5);

                INSERT INTO Cars (DealerId, Make, Model, Year, StockCount) VALUES (2, 'Honda', 'Civic', 2003, 7);
                INSERT INTO Cars (DealerId, Make, Model, Year, StockCount) VALUES (2, 'Honda', 'Accord', 2020, 3);

                INSERT INTO Cars (DealerId, Make, Model, Year, StockCount) VALUES (3, 'Ford', 'Fusion', 1995, 8);
                INSERT INTO Cars (DealerId, Make, Model, Year, StockCount) VALUES (3, 'Ford', 'Mustang', 2001, 2);

                INSERT INTO Cars (DealerId, Make, Model, Year, StockCount) VALUES (4, 'Chevrolet', 'Malibu', 2011, 6);
                INSERT INTO Cars (DealerId, Make, Model, Year, StockCount) VALUES (4, 'Chevrolet', 'Camaro', 2023, 4);

                INSERT INTO Cars (DealerId, Make, Model, Year, StockCount) VALUES (5, 'Nissan', 'Altima', 1995, 9);
                INSERT INTO Cars (DealerId, Make, Model, Year, StockCount) VALUES (5, 'Nissan', 'Maxima', 2001, 1);

            ");
        }
    }
}