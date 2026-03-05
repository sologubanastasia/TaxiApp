using TaxiApp.Domain;
namespace TaxiApp.Application.Interfaces
{
    public interface ITripRepository
    {
        Task InitializeDatabaseAsync();
        Task BulkInsertAsync(List<Trip> trips);
    }
}