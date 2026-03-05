using TaxiApp.Domain
namespace TaxiApp.Application.Interfaces
{
    public interface ITripRepository
    {
        Task InitializeDatabase();
        Task BulkInsert(List<Trip> trips);
    }
}