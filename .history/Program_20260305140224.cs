using TaxiApp.Application.Services;
using TaxiApp.Infrastructure.Readers;
using TaxiApp.Infrastructure.Repositories;

var reader = new CsvTripReader();
var repositor = new SqlTripRepository(connectionString);
var service