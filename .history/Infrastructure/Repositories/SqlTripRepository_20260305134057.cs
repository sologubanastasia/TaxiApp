namespace TaxiApp.Infrastructure.Repositories
{
    public class SqlTripRepository : ITripRepository
    {
        private readonly string _connectionString;
        public SqlTripRepository(string connectionString)
    }
}