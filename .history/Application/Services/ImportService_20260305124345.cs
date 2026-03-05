namespace TaxiApp.Appliation.Services
{
    public class ImpostService
    {
        private readonly ICsvReader _reader;
        private readonly ITripRepository _repo;

        public ImpostService(ICsvReader reader, ITripRepository repo)
        {
            _reader = reader;
            _repo = repo;
        }

        public async Task ExecuteAsync(string filePath)
        {
            await _repo.InitializeAsync();

            var (data, dublicates) = await _ reader.ReadAllAsync(filePath);

            await SaveDublicatesAsync(dublicates);

            await _repo.BulkInsertAsync(data);
            Console.WriteLine($"Added: {data.Count}, dublicates: {dublicates.Count}");
        }
    }
    private async Task SaveDublicatesAsync(List)
}