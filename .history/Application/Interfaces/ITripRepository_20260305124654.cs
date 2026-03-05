using TaxiApp.Domain;
namespace TaxiApp.Application.Interfaces
{
    public interface ITripRepository
    {
        Task InitializeDatabaseAsynv();
        Task BulkInsert(List<Trip> trips);
    }
}