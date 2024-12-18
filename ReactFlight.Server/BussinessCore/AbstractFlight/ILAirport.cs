using ReactFlight.Server.Model.Product.Flight;

namespace ReactFlight.Server.BussinessCore.AbstractFlight
{
    public interface ILAirport
    {
        public List<string> GET_AirportAutoComplete(string prefix);
    }
}
