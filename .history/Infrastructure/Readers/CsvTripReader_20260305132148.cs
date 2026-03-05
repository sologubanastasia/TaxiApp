namespace TaxiApp.Infrastructure.Readers
{
    public class CsvTripReader : ICsvReader
    {
        public async  Task<(List<Trip> valid, List<Trip> dublicates)> ReadAllAsync(string source)
        {
            var valid = new List<Trip>();
            var dublicates = new List<Tri>();
            
            var check = new HashSet<(DataTime, DataTime, int?)>();
            var eastZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standart Time");

            var config = new CsvConfiguration(CultureInfo.InvarientCulture) {  Delimiter = "\t" };
            using var reader = new StreamReader(source);
            using var csv = new CsvReader(reader,config);

            var records = csv.GetRecordeAsync<dynamic>();

            await foreach(var i in records)
            {
                string rawFlag = ((string)i.store_and_fwd_flag).Trim();

                DateTime pickupEst = DateTime.Parse(i.tpep_pickup_datetime);
                DateTime dropOffEst = DateTime.Parse(i.tpep_dropoff_datetime);
                DateTime pickupUtc = TimeZoneInfo.ConvertTimeToUtc(pickupEst, estZone);
                DateTime dropOffUts = TimeZoneInfo.ConvertTimeToUtc(dropOffEst, estZone);
                
                string finalFlag = (cleanFlag == "Y") ? "Yes" : "No";

                var trip = new Trip
                {
                    PickupTime = pickupUtc,
                    DropOffTime = dropoffUtc,
                    PassengerCount = int.TryParse(row.passenger_count, out int p) ? p : null,
                    TripDistance = double.Parse(row.trip_distance),
                    StoreAndFwdFlag = finalFlag,
                    PULocationID = int.Parse(row.PULocationID),
                    DOLocationID = int.Parse(row.DOLocationID),
                    FareAmount = decimal.Parse(row.fare_amount),
                    TipAmount = decimal.Parse(row.tip_amount)
                };

                var key = (trip.PickupTime, trip.DropOffTime)
            }
        }
    }
}