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
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            using var bulk = new SqlBulkCopy(connection) 
            {
                DestinationTableName = "Trips";
            }
            await bulk.WriteToServerAsync(ToDataTable(trips));
        }

        private DataTable ToDataTable(List<Trip> trips)
        {
            var data = new DataTable();
            data.Columns.Add("tpep_pickup_datetime", typeof(DateTime));
            data.Columns.Add("tpep_dropoff_datetime", typeof(DateTime));
            data.Columns.Add("passenger_count", typeof(int));
            data.Columns.Add("trip_distance", typeof(double));
            data.Columns.Add("store_and_fwd_flag", typeof(string));
            data.Columns.Add("PULocationID", typeof(int));
            data.Columns.Add("DOLocationID", typeof())
        }
    }
    tpep_pickup_datetime`
    - `tpep_dropoff_datetime`
    - `passenger_count`
    - `trip_distance`
    - `store_and_fwd_flag`
    - `PULocationID`
    - `DOLocationID`
    - `fare_amount`
    - `tip_amount`
}