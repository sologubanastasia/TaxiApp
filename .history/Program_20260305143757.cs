using TaxiApp.Appliation.Services;
using TaxiApp.Infrastructure.Readers;
using TaxiApp.Infrastructure.Repositories;

string connectionString = "Server=127.0.0.1,1433;Database=TaxiDb;User Id=sa;Password=TaxiSecretPassword123@;TrustServerCertificate=True;Connect Timeout=30;";
var reader = new CsvTripReader();
var repository = new SqlTripRepository(connectionString);
var service = new ImportService(reader, repository);

await service.ExecuteAsync("data.csv");