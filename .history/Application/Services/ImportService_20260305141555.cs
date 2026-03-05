using TaxiApp.Domain.Models;
using TaxiApp.Application..Interfaces;
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

            var (data, dublicates) = await _ reader.ReadAllAsync(filePath);

            await SaveDublicatesAsync(dublicates);

            await _repo.BulkInsertAsync(data);
            Console.WriteLine($"Added: {data.Count}, dublicates: {dublicates.Count}");
        }
        
    }
}