using TaxiApp.Domain.Models;
using TaxiApp.Application.Interfaces;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

namespace TaxiApp.Infrastructure.Readers
{
    public class CsvTripReader : ICsvReader
    {
        public async Task<(List<Trip> data, List<Trip> duplicates)> ReadAllAsync(string source)
        {
            var data = new List<Trip>();
            var duplicates = new List<Trip>();
            
            var check = new HashSet<(DateTime, DateTime, int?)>();
            var eastZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");

            var config = new CsvConfiguration(CultureInfo.InvariantCulture) 
            {
                Delimiter = ",",
                PrepareHeaderForMatch = args => args.Header.Trim().ToLower(),
                HeaderValidated = null, 
                MissingFieldFound = null
            };
            
            using var reader = new StreamReader(source);
            using var csv = new CsvReader(reader, config);

            var records = csv.GetRecordsAsync<dynamic>();

            await foreach(var r in records)
            {
                var i = (IDictionary<string, object>)r;
                
                string flag = (i["store_and_fwd_flag"]?.ToString() ?? "").Trim();

                DateTime pickupEst = DateTime.Parse(i["tpep_pickup_datetime"].ToString()!);
                DateTime dropOffEst = DateTime.Parse(i["tpep_dropoff_datetime"].ToString()!);
                
                DateTime pickupUtc = TimeZoneInfo.ConvertTimeToUtc(pickupEst, eastZone);
                DateTime dropOffUtc = TimeZoneInfo.ConvertTimeToUtc(dropOffEst, eastZone);
                
                string finalFlag = (flag == "Y") ? "Yes" : "No";

                var trip = new Trip
                {
                    PickupTime = pickupUtc,
                    DropOffTime = dropOffUtc,
                    PassengerCount = int.TryParse(i["passenger_count"]?.ToString(), out int p) ? p : null,
                    TripDistance = double.Parse(GetValue(i, "trip_distance") 
                    StoreAndFwdFlag = finalFlag,
                    PULocationID = int.Parse(i["pulocationid"]?.ToString() ?? "0"),
                    DOLocationID = int.Parse(i["dolocationid"]?.ToString() ?? "0"),
                    FareAmount = decimal.Parse(i["fare_amount"]?.ToString() ?? "0"),
                    TipAmount = decimal.Parse(i["tip_amount"]?.ToString() ?? "0")
                };

                var uniqueKey = (trip.PickupTime, trip.DropOffTime, trip.PassengerCount);

                if(check.Add(uniqueKey)) {
                    data.Add(trip);
                }
                else {
                    duplicates.Add(trip);
                }
            }
            return (data, duplicates);
        }
    }
}