using TaxiApp.Domain;
namespace TaxiApp.Application.Interfaces
{
    public interface ITripRepository
    {
        Task InitializeD();
        Task BulkInsert(List<Trip> trips);
    }
}