using ReactFlight.Server.Model.Miscellaneous;
using ReactFlight.Server.Model.Product.Flight;
using ReactFlight.Server.Model.User;

namespace ReactFlight.Server.Model.Product
{
    public class ItineraryDTO
    {
        public string? Email { get; set; } = string.Empty;
        public string? ContactNo { get; set; } = string.Empty;
        public string? CountryDialingCode { get; set; } = string.Empty;
        public string? Key { get; set; } = string.Empty;
        public string? Token { get; set; } = string.Empty;
        public string? Supp { get; set; } = string.Empty;
        public string? TripType { get; set; } = string.Empty;
        public string? AccountCode { get; set; }
        public string? CompanyCode { get; set; }
        public string? WebsiteName { get; set; }
        public string? BookingCode { get; set; }
        public string? TicketInfo { get; set; }
        public string? BookingRef { get; set; }
        public string? ProductStatus { get; set; }
        public string? BookingStatus { get; set; }
        public string? ProdRef { get; set; }
        public string? ProdBookingId { get; set; }
        public string? SupplierRef { get; set; }
        public string? Origin { get; set; }
        public string? Destination { get; set; }
        public string? Currency { get; set; }
        public string? AdvertMedia { get; set; }
        public string? AgentRef { get; set; }
        public string? AirlineRef { get; set; }
        public string? BookingMedia { get; set; }
        public string? AuditID { get; set; }
        public string? MetaSearch { get; set; }
        public Dictionary<string, string>? BookingRefRange { get; set; } = new Dictionary<string, string>();
        public Dictionary<string, DateTime?> BookingDateRange { get; set; } = new Dictionary<string, DateTime?>();
        public Dictionary<string, DateTime?> DepartDateRange { get; set; } = new Dictionary<string, DateTime?>();
        public Dictionary<string, DateTime?> OptionDateRange { get; set; } = new Dictionary<string, DateTime?>();
        public Dictionary<string, DateTime?> PaymentDateRange { get; set; } = new Dictionary<string, DateTime?>();
        public Dictionary<string, DateTime?> BankingDateRange { get; set; } = new Dictionary<string, DateTime?>();
        public Dictionary<string, DateTime?> IssuedDateRange { get; set; } = new Dictionary<string, DateTime?>();
        public DateTime? NULLDate { get; set; }
        public List<AirSegmentInfo>? AirSegments { get; set; }
        public List<PaxDTO> PaxInfos { get; set; } = new();
        public AccountDTO AccountInfo { get; set; } = new AccountDTO();
        public PNRInfoDTO PNRInfo { get; set; } = new();
        public string? BookingDate { get; internal set; }
        public string? BookingStatusDate { get; internal set; }
        public bool? IsClearedFromQC { get; internal set; }
        public bool? IsBreakDownShow { get; internal set; }
        public DateTime? PaymentDate { get; internal set; }
        public string? PaymentRemarks { get; internal set; }
        public string? Booking_Approval_Status { get; internal set; }
        public string? SecureString { get; internal set; }
        public string? BookingType { get; internal set; }
        public PricingDTO? PricingInfo { get; internal set; }
        public LogDetailDTO? LogInfo { get; internal set; }
        public TransactionDTO? TransactionInfo { get; internal set; }
        public ErrorLogDTO? ErrorInfo { get; internal set; }
    }
}
