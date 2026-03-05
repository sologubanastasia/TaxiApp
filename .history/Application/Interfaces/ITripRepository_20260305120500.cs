namespace TaxiApp.Application.Interfaces
{
    public interface ITripRepository
    {
        Task InitializeDatabase();
         BulkInsert(List<Trip> trips);
    }
}