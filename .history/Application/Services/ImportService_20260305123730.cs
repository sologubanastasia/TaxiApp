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
    }
}