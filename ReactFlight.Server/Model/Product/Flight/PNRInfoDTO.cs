namespace ReactFlight.Server.Model.Product.Flight
{
    public class PNRInfoDTO
    {
        public string? CreationDate { get; set; } = string.Empty;
        public string? RecLoc { get; set; }
        public string? UniversalRecLoc { get; set; } = string.Empty;
        public string? RecLocDisplay { get; set; } = string.Empty;
        public string? AirLocatorCode { get; set; } = string.Empty;
        public string? ProviderCode { get; set; } = string.Empty;
        public string? AirlineRef { get; set; } = string.Empty;
        public string? AgencyIATANum { get; set; } = string.Empty;
        public string? FileAddress { get; set; } = string.Empty;
        public string? AgencyName { get; set; } = string.Empty;
        public string? AgencyPCC { get; set; } = string.Empty;
        public string? CurAgencyPCC { get; set; } = string.Empty;
        public string? IsTicketed { get; set; } = string.Empty;
        public string? AgentSignon { get; set; } = string.Empty;
        public string? OriginalReceivedFieldValue { get; set; } = string.Empty;
        public string? PnrIssuelastDate { get; set; } = string.Empty;
        public string? BrightsunReference { get; set; }
        public decimal totalCost { get; set; }
    }
}
