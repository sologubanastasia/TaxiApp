using TaxiApp.Domain.Models;
namespace TaxiApp.Application.Interfaces
{
    public interface ICsvReader
    {
        Task<(List<Trip> data, List<Trip> dublicates)> ReadAllAsync(string source);
    }
}