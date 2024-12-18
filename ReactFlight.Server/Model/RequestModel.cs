using System.Data;

namespace ReactFlight.Server.Model
{
    public class RequestModel
    {
        public string? TripType { get; set; }
        public string? Origin { get; set; }
        public string? Destination { get; set; }
        public string? DepartDate { get; set; }
        public string? ArrivalDate { get; set; }
        public string? Class { get; set; }
        public bool? IsFlexibleDate { get; set; }
        public bool? IsDirectFlight { get; set; }
        public string? OutBoundKey { get; internal set; }
        public string? InBoundKey { get; set; }
        public string[] OptionKeyList { get; set; } = new string[0];
        public int NoOfInfantPax { get; set; }
        public int NoOfAdultPax { get; set; }
        public int NoOfChildPax { get; set; }
        public int NoOfYouthPax { get; set; }
        public string? Supp { get; set; }
        public string? CompanyCode { get; set; }
        public string? SubCompanyCode { get; set; }
        public string? WebsiteName { get; set; }
        public string? AirlineCode { get; set; }
        public string? AccountCode { get; set; }
        public string? Token { get; set; }
        public string? Key { get; set; }
        public List<PaxDTO>? PaxInfoList { get; set; } = new();
        public List<AirSolution>? AirSolution { get; set; } = new();
    }

    public partial class PaxDTO
    {
        public string? PaxType { get; set; }
        public string? Title { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        //public string? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string? Email { get; set; }
        public string? MobileNumber { get; set; }
        public string? FrequentFlyer { get; set; }
        public string? UserID { get; set; }
        public string? ContactNo { get; set; }
        public string? LandlineNo { get; set; }
        public string? CompCode { get; set; }
        public bool? IsLeadName { get; internal set; }
        public int? PaxId { get; internal set; }
        public string? PassportNo { get; internal set; }
        public DateTime? PaxDOB { get; internal set; }
        public DateTime? PCR_TestDateTime { get; internal set; }
    }
}
