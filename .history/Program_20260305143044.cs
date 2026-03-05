using TaxiApp.Appliation.Services;
using TaxiApp.Infrastructure.Readers;
using TaxiApp.Infrastructure.Repositories;

var connectionString = Server
var reader = new CsvTripReader();
var repository = new SqlTripRepository(connectionString);
var service = new ImportService(reader, repository);

await service.ExecuteAsync("data.csv");