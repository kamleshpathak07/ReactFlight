namespace ReactFlight.Server.Model.Miscellaneous
{
    public class LogDetailDTO
    {
        public string BookingRef { get; set; } = string.Empty;
        public string LogId { get; set; } = string.Empty;
        public string? AccessedBy { get; set; } = string.Empty;
        public string? AccessedByType { get; set; } = string.Empty;
        public string? LogDescript { get; set; } = string.Empty;
        public string? Country { get; set; } = string.Empty;
        public string? City { get; set; } = string.Empty;
        public string? DNS { get; set; } = string.Empty;
        public string? AccessedIP { get; set; }
        public DateTime LoginDateTime { get; set; }
        public string? UserBrowser { get; set; }
        public string? SessionId { get; set; }
        public string? AccessedURL { get; set; }
        public string? Action { get; set; }
        public string? ActionDescript { get; set; }
        public string? QueryString { get; set; }
        public string? UrlReferrer { get; set; }
        public string? ClientNotes { get; set; }
    }
}
