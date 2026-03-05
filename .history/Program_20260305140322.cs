using TaxiApp.Application.Services;
using TaxiApp.Infrastructure.Readers;
using TaxiApp.Infrastructure.Repositories;

var reader = new CsvTripReader();
var repository = new SqlTripRepository(connectionString);
var service = new ImportService(reader, repository);

await service