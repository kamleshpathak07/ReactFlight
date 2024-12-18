namespace ReactFlight.Server.Model.User
{
    public class UserDTO
    {
        public bool isMainAcc { get; set; } = false;
        public int NoOfPax { get; set; } = 0;
        public int NoOfAdultPax { get; set; } = 0;
        public int NoOfYouthPax { get; set; } = 0;
        public int NoOfChildPax { get; set; } = 0;
        public int NoOfInfantPax { get; set; } = 0;
        public int? PaxAge { get; set; }
        public string? PaxType { get; set; } = string.Empty;
        public string? Title { get; set; } = string.Empty;
        public string? FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
        public string? UserID { get; set; } = string.Empty;
        public bool? IsLeadName { get; set; }
        public string? MiddelName { get; set; } = string.Empty;
        public int? PaxId { get; set; }
        public string? Gender { get; set; } = string.Empty;
        public string? PassportNo { get; set; } = string.Empty;
        public DateTime? PaxDOB { get; set; }
        public DateTime? PCR_TestDateTime { get; set; }
        // public List<TicketDTO>? TicketInfos { get; set; }
        public string? ContactNo { get; set; } = string.Empty;
        public string? LandlineNo { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public string? Fax { get; set; } = string.Empty;
        public string? FrequestFlyerNo { get; set; } = string.Empty;
        public string? SeatPref { get; set; } = string.Empty;
        public string? MealPref { get; set; } = string.Empty;
        public string? Assitance { get; set; } = string.Empty;
        public string? BilHouseNo { get; set; } = string.Empty;
        public string? BilStreet { get; set; } = string.Empty;
        public string? BilPcode { get; set; } = string.Empty;
        public string? BilLphone { get; set; } = string.Empty;
        public string? BilCity { get; set; } = string.Empty;
        public string? BilCountry { get; set; } = string.Empty;
        public string? BilMob { get; set; } = string.Empty;
        // public MileageDTO? MileageInfo { get; set; }
        public string? SeatNumber { get; set; } = string.Empty;
        public string? OriginAndDestinationForSeat { get; set; } = string.Empty;
        public string? LeadName { get; set; } = string.Empty;
        public string? AgentPassword { get; set; } = string.Empty;
        public string? CompCode { get; set; } = string.Empty;
        public string? BusinessCurrency { get; set; } = string.Empty;
        public decimal CreditLimit { get; set; }
        public decimal DueToPay { get; set; }
        public decimal CreditBalance { get; set; }
        public string? AccountCode { get; set; } = string.Empty;
        public string? AccessType { get; set; } = string.Empty;
        public string? CompName { get; set; } = string.Empty;
        public string? FlightMarkupAP { get; set; } = string.Empty;
        public double FlightMarkupAmt { get; set; }
        public string? HotelMarkupAP { get; set; } = string.Empty;
        public decimal? HotelMarkupAmt { get; set; }
        public string? InsuranceMarkupAP { get; set; } = string.Empty;
        public double? InsuranceMarkupAmt { get; set; }
        public string? CarMarkupAP { get; set; } = string.Empty;
        public double? CarMarkupAmt { get; set; }
        public string? PackageMarkupAP { get; set; } = string.Empty;
        public double? PackageMarkupAmt { get; set; }
        public bool AllowOptionBooking { get; set; }
        public int Otp { get; set; } = 0;
        public DateTime? OtpDate { get; set; }
        public string? OtpEmailId { get; set; } = string.Empty;
    }
}
