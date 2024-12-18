using ReactFlight.Server.Model.Product.Flight;

namespace ReactFlight.Server.Model.User
{
    public class AccountDTO
    {
        public string? CompanyCode { get; set; } = string.Empty;
        public string? AccountType { get; set; } 
        public string? AccountCode { get; set; } 
        public string? CompanyName { get; set; } = string.Empty;
        public string? CompanyEmail { get; set; } = string.Empty;
        public UserDTO? UserInfo { get; set; } = new UserDTO();
        public string? IsActive { get; set; }
        public CityDTO? City { get; set; } = new CityDTO();
        public CountryDTO? Country { get; set; } = new CountryDTO();
        public string? State { get; set; } = string.Empty; 
        public DateTime? ToDepart { get; set; } = new DateTime();
        public DateTime? ToBooked { get; set; } = new DateTime();
        public DateTime? FromBooked { get; set; } = new DateTime();
        public DateTime? FromIssued { get; set; } = new DateTime();
        public DateTime? ToIssued { get; set; } = new DateTime();
       // public Log LoginInfo { get; internal set; }
    }
}
