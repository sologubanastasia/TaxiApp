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
            await con
        }
    }
}