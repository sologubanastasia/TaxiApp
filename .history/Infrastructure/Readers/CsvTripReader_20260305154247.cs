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

            var config = new CsvConfiguration(CultureInfo.InvariantCulture) { Delimiter = "\t" };
            using var reader = new StreamReader(source);
            using var csv = new CsvReader(reader, config);

            var records = csv.GetRecordsAsync<dynamic>();

            await foreach(var i in records)
            {
                string flag = (i[].store_and_fwd_flag).Trim();

                DateTime pickupEst = DateTime.Parse(i.["tpep_pickup_datetime"].ToString()!);
                DateTime dropOffEst = DateTime.Parse(i.["tpep_dropoff_datetime"].ToString()!);
                DateTime pickupUtc = TimeZoneInfo.ConvertTimeToUtc(pickupEst, eastZone);
                DateTime dropOffUtc = TimeZoneInfo.ConvertTimeToUtc(dropOffEst, eastZone);
                
                string finalFlag = (flag == "Y") ? "Yes" : "No";

                var trip = new Trip
                {
                    PickupTime = pickupUtc,
                    DropOffTime = dropOffUtc,
                    PassengerCount = int.TryParse((string)i.passenger_count, out int p) ? p : null,
                    TripDistance = double.Parse((string)i.trip_distance),
                    StoreAndFwdFlag = finalFlag,
                    PULocationID = int.Parse((string)i.PULocationID),
                    DOLocationID = int.Parse((string)i.DOLocationID),
                    FareAmount = decimal.Parse((string)i.fare_amount),
                    TipAmount = decimal.Parse((string)i.tip_amount)
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