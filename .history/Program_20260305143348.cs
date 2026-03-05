using TaxiApp.Appliation.Services;
using TaxiApp.Infrastructure.Readers;
using TaxiApp.Infrastructure.Repositories;

string connectionString = "Server=localhost,1443;Database=TaxiDb;User Id=sa;Password=TaxiSecretPassword123@;TrustServerCertificate=True;";
var reader = new CsvTripReader();
var repository = new SqlTripRepository(connectionString);
var service = new ImportService(reader, repository);

await service.ExecuteAsync("data.csv");