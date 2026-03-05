using TaxiApp.Appliation.Services;
using TaxiApp.Infrastructure.Readers;
using TaxiApp.Infrastructure.Repositories;
using Microsoft.
var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json").Build();

string connectionString = configuration.GetConnectionString("DefaultConnection");

var reader = new CsvTripReader();
var repository = new SqlTripRepository(connectionString);
var service = new ImportService(reader, repository);

await service.ExecuteAsync("data.csv");