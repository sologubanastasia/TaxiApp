nammespace TaxiApp.Domain
{
    public class Trip
    {
        public DateTime PickupTime { get; set; }
        public DateTime DropOffTime { get; set; }
        public int? PassengerCount { get; set; }
        public double TripDistance { get; set; }
        public string StoreAndFwdFlag { get; set; }
        public int PULocationID { get; set; }
        public int DOLocation { get; set; }
        public decimal FareAmount { get; set; }
        public decimal TipAmount { get; set; }
    }
}
