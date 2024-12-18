namespace ReactFlight.Server.Model.Product.Flight
{
    public class AirportDTO
    {
        public string? AirportName { get; set; }
        public string? AirportCode { get; set; }
        public CityDTO? City { get; set; }
        public CountryDTO? Country { get; set; }
    }
}
