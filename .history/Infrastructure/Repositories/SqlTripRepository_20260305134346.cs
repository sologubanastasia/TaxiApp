namespace TaxiApp.Infrastructure.Repositories
{
    public class SqlTripRepository : ITripRepository
    {
        private readonly string _connectionString;

        public SqlTripRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task InitializeDatabaseAsync()
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            string sql = @"
            IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Trips')
            BEGIN
                CREATE TABLE Trips (
                    tpep_pickup_datetime DATETIME2,
                    tpep_dropoff_datetime DATETIME2,
                    passenger_count INT,
                    trip_distance FLOAT,
                    store_and_fwd_flag VARCHAR(3),
                    PULocationID INT,
                    DOLocationID INT,
                    fare_amount DECIMAL(18,2),
                    tip_amount DECIMAL(18,2)
                );
                CREATE INDEX IX_PULocationID ON Trips(PULocationID); -- Для пошуку за ID
                CREATE INDEX IX_Distance ON Trips(trip_distance DESC); -- Для топ 100 дистанцій
            END";

            await new SqlCommand(sql, connection).ExecuteNonQueryAcync();
        }

        public async Task BulkInsertAsync(List<Trip> trips)
        {
            
        }
    }
}