using TaxiApp.Domain;
namespace TaxiApp.Application.Interfaces
{
    public interface ITripRepository
    {
        Task InitializeDatabaseAsync();
        Task BulkInsert(List<Trip> trips);
    }
}