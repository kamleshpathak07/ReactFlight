namespace ReactFlight.Server.BussinessCore.Common
{
    public class Credential
    {
        public BST? BST
        {
            get
            {
                return new BST();
            }
            set
            {
                BST = value;
            }
        }
    }
    public struct BST
    {
        public string BaseUrl => "https://api.brightsun.co.uk/api/BSFlight/";
        public string FlightSearch => "flightsearch";
        public string FlightPrice => "flightprice";
        public string PnrCreate => "flightpnr";
        public string Websitename => "Btpremier.com";
        public string AccountCode => "BS3555";
    }
}
