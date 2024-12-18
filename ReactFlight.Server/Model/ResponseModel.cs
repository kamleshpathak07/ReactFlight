using System.Data;

namespace ReactFlight.Server.Model
{
    public class ResponseModel
    {
        public Result? Result { get; set; }
        public int Id { get; set; }
        public int Status { get; set; }
        public bool IsCanceled { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsCompletedSuccessfully { get; set; }
        public int CreationOptions { get; set; }
        public bool IsFaulted { get; set; }
    }

    public class Airport
    {
        public string? AirportCode { get; set; }
        public string? AirportName { get; set; }
        public City? City { get; set; }
        public string? DestinationTerminal { get; set; }
        public string? OriginTerminal { get; set; }
    }

    public class AirSegmentInfo
    {
        public string? AirlineLogoUrl { get; set; }
        public string? Origin { get; set; }
        public string? Destination { get; set; }
        public string? FlightNumber { get; set; }
        public string? DepartDatetime { get; set; }
        public string? ArrivalDatetime { get; set; }
        public string? DepartDate { get; set; }
        public string? ArrivalDate { get; set; }
        public string? DepartTime { get; set; }
        public string? ArrivalTime { get; set; }
        public string? SubClass { get; set; }
        public string? Carrier { get; set; }
        public string? FlightTime { get; set; }
        public string? ChangeOfPlane { get; set; }
        public List<Airport>? Airport { get; set; }
        public BaggageInfo? BaggageInfo { get; set; }
        public string? SeatsLeft { get; set; }
        public string? CabinClass { get; set; }
        public string? OriginAirportName { get; set; }
        public string? DestinationAirportName { get; set; }
        public string? TravelDuration { get; set; }
        public string? AirlineName { get; set; }
        public string? OriginAirportCity { get; set; }
        public string? OriginAirportCountry { get; set; }
        public string? DestinationAirportCity { get; set; }
        public string? DestinationAirportCountry { get; set; }
        public string? TicketCarrier { get; set; }
        public string? ConnectionTime { get; set; }
        public int SegmentId { get; internal set; }
        public string? MainOrigin { get; internal set; }
        public string? MainDestination { get; internal set; }
        public string? AirportTerminal { get; internal set; }
        public string? BaggageDetails { get; internal set; }
        public string? FareBasis { get; internal set; }
        public string? Status { get; internal set; }
        public string? SegmentRemarks { get; internal set; }
        public bool IsSegRemarkVisibleToCust { get; internal set; }
        public string? OperatingCarrier { get; internal set; }
        public string? OperatingFlightNumber { get; internal set; }
        public int NoOfStops { get; internal set; }
        public string? Class { get; internal set; }
    }

    public class AirSolution
    {
        public string? Key { get; set; }
        public double? TotalPrice { get; set; }
        public double? BasePrice { get; set; }
        public double Tax { get; set; }
        public List<Journey>? Journey { get; set; }
        public string? Provider { get; set; }
        public List<FareRule>? FareRule { get; set; }
        public bool PrivateFare { get; set; }
        public List<PricingInfo>? PricingInfos { get; set; }
        public string? Supp { get; set; }
        public string? ChangePenalty { get; set; }
        public string? CancelPenalty { get; set; }
        public string? FareBasis { get; set; }
        public decimal? MrkValue { get; set; }
    }

    public class BaggageInfo
    {
        public string? Allowance { get; set; }
    }

    public class City
    {
        public string? CityCode { get; set; }
        public string? CityName { get; set; }
    }

    public class DistinctAirline
    {
        public string? AirlineCode { get; set; }
        public string? AirlineName { get; set; }
        public string? AirlineLogo { get; set; }
    }

    public class DistinctAirport
    {
        public string? AirportCode { get; set; }
        public string? AirportName { get; set; }
        public string? DestinationTerminal { get; set; }
        public City? City { get; set; } = new City();
        public string? originTerminal { get; set; }
    }

    public class FareRule
    {
        public string? PaxType { get; set; }
        public string? FareRuleKey { get; set; }
        public string? FareInfoRef { get; set; }
        public string? ProviderCode { get; set; }
    }

    public class Journey
    {
        public int Group { get; set; }
        public string? OutBoundKey { get; set; }
        public List<AirSegmentInfo>? AirSegments { get; set; }
        public List<OptionInfo>? OptionInfos { get; set; }
        public string? LegRef { get; set; }
        public string? Destination { get; set; }
        public string? Origin { get; set; }
        public int? Stop { get; set; }
        public string? InboundKey { get; set; }
    }

    public class OptionInfo
    {
        public string? OptionKey { get; set; }
        public string? TotalFlightDuration { get; set; }
        public List<AirSegmentInfo>? AirSegmentInfos { get; set; }
        public int? Stop { get; set; }
        public decimal? MrkValue { get; set; }
    }

    public class PricingInfo
    {
        public string? PaxType { get; set; }
        public string? PaxTypeName { get; set; }
        public int? NoOfPax { get; set; }
        public double? TotalPrice { get; set; }
        public double? BasePrice { get; set; }
        public double? Tax { get; set; }
    }

    public class Result
    {
        public string? Status { get; set; }
        public string? Token { get; set; }
        public List<AirSolution>? AirSolutions { get; set; }
        public List<DistinctAirport>? DistinctAirports { get; set; }
        public List<DistinctAirline>? DistinctAirlines { get; set; }
    }
}
