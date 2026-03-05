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
                
                string flag = (GetValue(i, "store_and_fwd_flag") ?? "").Trim();

                DateTime pickupEst = DateTime.Parse(GetValue(i, "tpep_pickup_datetime") ?? DateTime.Now.ToString(), CultureInfo.InvariantCulture);
                DateTime dropOffEst = DateTime.Parse(GetValue(i, "tpep_dropoff_datetime") ?? DateTime.Now.ToString(), CultureInfo.InvariantCulture);
                DateTime pickupUtc = TimeZoneInfo.ConvertTimeToUtc(pickupEst, eastZone);
                DateTime dropOffUtc = TimeZoneInfo.ConvertTimeToUtc(dropOffEst, eastZone);
                
                string finalFlag = (flag == "Y") ? "Yes" : "No";

                var trip = new Trip
                {
                    PickupTime = pickupUtc,
                    DropoffTime = dropOffUtc, 
                    PassengerCount = int.TryParse(GetValue(i, "passenger_count"), out int p) ? p : null,
                    TripDistance = double.Parse(GetValue(i, "trip_distance") ?? "0", CultureInfo.InvariantCulture),
                    StoreAndFwdFlag = finalFlag,
                    PULocationID = int.Parse(GetValue(i, "pulocationid") ?? "0"),
                    DOLocationID = int.Parse(GetValue(i, "dolocationid") ?? "0"),
                    FareAmount = decimal.Parse(GetValue(i, "fare_amount") ?? "0", CultureInfo.InvariantCulture),
                    TipAmount = decimal.Parse(GetValue(i, "tip_amount") ?? "0", CultureInfo.InvariantCulture)
                };

                var uniqueKey = (trip.PickupTime, trip.DropoffTime, trip.PassengerCount);

                if(check.Add(uniqueKey)) {
                    data.Add(trip);
                }
                else {
                    duplicates.Add(trip);
                }
            }
            return (data, duplicates);
        }

        private string? GetValue(IDictionary<string, object> dict, string key)
        {
            return dict.FirstOrDefault(x => x.Key.Trim().ToLower() == key.ToLower()).Value?.ToString();
        }
    }
}