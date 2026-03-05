namespace TaxiApp.Application.Interfaces
{
    public interface ITripRepository
    {
        Task InitializeDatabase();
        void BulkInsert(List<Trip> trips);
    }
}