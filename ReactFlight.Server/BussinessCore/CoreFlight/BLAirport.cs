using ReactFlight.Server.BussinessCore.AbstractFlight;
using ReactFlight.Server.InfraLayer.Product.Flight;
using ReactFlight.Server.Model.Product.Flight;

namespace ReactFlight.Server.BussinessCore.CoreFlight
{
    public class BLAirport : ILAirport
    {
        private readonly DLAirport _dLAirport;
        public BLAirport(DLAirport dLAirport)
        {
            _dLAirport = dLAirport;
        }
        public List<string> GET_AirportAutoComplete(string prefix)
        {
            List<string> result = new List<string>();
            List<AirportDTO> airportDTOs = new List<AirportDTO>();
            airportDTOs = _dLAirport.Get_Airport_AutoComplete(prefix) ?? new List<AirportDTO>();
            airportDTOs.ForEach(airportDTO => result.Add(airportDTO.AirportName + "[" + airportDTO.AirportCode + "]" + "," + airportDTO.Country?.CountryName));
            return result;
        }
    }
}
