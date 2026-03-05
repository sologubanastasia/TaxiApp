namespace TaxiApp.Infrastructure.Readers
{
    public class CsvTripReader : ICsvReader
    {
        public async  Task<(List<Trip> data, List<Trip> dublicates)> ReadAllAsync(string source)
        {
            var data = new List<Trip>();
            var dublicates = new List<Trip>();
            
            var check = new HashSet<(DateTime, DateTime, int?)>();
            var eastZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standart Time");

            var config = new CsvConfiguration(CultureInfo.InvariantCulture) {  Delimiter = "\t" };
            using var reader = new StreamReader(source);
            using var csv = new CsvReader(reader,config);

            var records = csv.GetRecordsAsync<dynamic>();

            await foreach(var i in records)
            {
                string flag = ((string)i.store_and_fwd_flag).Trim();

                DateTime pickupEst = DateTime.Parse(i.tpep_pickup_datetime);
                DateTime dropOffEst = DateTime.Parse(i.tpep_dropoff_datetime);
                DateTime pickupUtc = TimeZoneInfo.ConvertTimeToUtc(pickupEst, estZone);
                DateTime dropOffUts = TimeZoneInfo.ConvertTimeToUtc(dropOffEst, estZone);
                
                string finalFlag = (flag == "Y") ? "Yes" : "No";

                var trip = new Trip
                {
                    PickupTime = pickupUtc,
                    DropOffTime = dropoffUtc,
                    PassengerCount = int.TryParse(i.passenger_count, out int p) ? p : null,
                    TripDistance = double.Parse(i.trip_distance),
                    StoreAndFwdFlag = finalFlag,
                    PULocationID = int.Parse(row.PULocationID),
                    DOLocationID = int.Parse(row.DOLocationID),
                    FareAmount = decimal.Parse(row.fare_amount),
                    TipAmount = decimal.Parse(row.tip_amount)
                };

                var uniqueData = (trip.PickupTime, trip.DropOffTime, trip.PassengerCount);

                if(seen.Add(uniqueData)) {
                    valid.Add(trip);
                }
                else {
                    dublicates.Add(trip);
                }
            }
            return (data, dublicates);
        }
    }
}