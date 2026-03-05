nammespace TaxiApp.Domain
{
    public class Trip
    {
        public DateTime PickupTime { get; set; }
        public DateTime DropOffTime { get; set; }
        public int? PassengerCount { get; set; }
        public double TripDistance { get; set; }
        public string Store
    }
}

tpep_pickup_datetime`
    - `tpep_dropoff_datetime`
    - `passenger_count`
    - `trip_distance`
    - `store_and_fwd_flag`
    - `PULocationID`
    - `DOLocationID`
    - `fare_amount`
    - `tip_amount`