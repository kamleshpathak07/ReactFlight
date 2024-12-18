using System.Diagnostics.Eventing.Reader;

namespace ReactFlight.Server.Model.Miscellaneous
{
    [Serializable]
    public class ErrorLogDTO
    {
        public string? LogID { get; set; }
        public string? LogName { get; set; }
        public string? LogType { get; set; }
        public string? LogStatus { get; set; }
        public string? LogDescription { get; set; }
    }
}
