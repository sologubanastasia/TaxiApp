namespace TaxiApp.Application.Interfaces
{
    public interface ITripRepository
    {
         InitializeDatabase();
        void BulkInsert(List<Trip> trips);
    }
}