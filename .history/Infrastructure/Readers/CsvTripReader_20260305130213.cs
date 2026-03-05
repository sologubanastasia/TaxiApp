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
                DateTime pickupUtc = TimeZoneInfo.ConvertTimeTo
            }
        }
    }
}