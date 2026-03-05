namespace TaxiApp.Infrastructure.Readers
{
    public class CsvTripReader : ICsvReader
    {
        public async  Task<(List<Trip> valid, List<Trip> dublicates)> ReadAllAsync(string source)
        {
            var valid = new List<TaxiTrip>();
            var dublicates = new List
        }
    }
}