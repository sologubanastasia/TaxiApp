namespace TaxiApp.Application.Interfaces
{
    public interface ITripRepository
    {
        void InitializeDatabase();
        void BulkInsert(List<Trip> trips);
    }
}