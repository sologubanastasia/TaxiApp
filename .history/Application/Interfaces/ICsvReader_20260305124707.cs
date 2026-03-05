namespace TaxiApp.Application.Interfaces
{
    public interface ICsvReader
    {
        Task<(List<Trip> valid, List<Trip> dublicates)> ReadAllAsync(string source);
    }
}