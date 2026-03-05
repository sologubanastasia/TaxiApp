using TaxiApp.Domain.Models;
using TaxiApp.Application.Interfaces;
using System.Globalization;
using CsvHelper;
namespace TaxiApp.Appliation.Services
{
    public class ImportService
    {
        private readonly ICsvReader _reader;
        private readonly ITripRepository _repo;

        public ImportService(ICsvReader reader, ITripRepository repo)
        {
            _reader = reader;
            _repo = repo;
        }

        public async Task ExecuteAsync(string filePath)
        {
            await _repo.InitializeDatabaseAsync();

            var (data, dublicates) = await _reader.ReadAllAsync(filePath);

            await SaveDublicatesAsync(dublicates);

            await _repo.BulkInsertAsync(data);
            Console.WriteLine($"✅ Successfully imported: {data.Count:N0} rows");
            Console.WriteLine($"Duplicates removed:    {dublicates.Count:N0} rows");
            Console.WriteLine($"Total time elapsed:    {watch.Elapsed.TotalSeconds:F2} seconds");
        }

        private async Task SaveDublicatesAsync(List<Trip> dublicates)
        {
            using var writer = new StreamWriter("dublicates.csv");
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            await csv.WriteRecordsAsync(dublicates);
        }
    }
}