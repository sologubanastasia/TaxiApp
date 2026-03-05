using TaxiApp.Domain.Models;
namespace TaxiApp.Application.Interfaces
{
    public interface ICsvReader
    {
        Task<(List<Trip> data, List<Trip> dulicates)> ReadAllAsync(string source);
    }
}