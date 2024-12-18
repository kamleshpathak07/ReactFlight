using ReactFlight.Server.InfraLayer.Product;
using ReactFlight.Server.InfraLayer.Product.Flight;
using ReactFlight.Server.Model.Product;

namespace ReactFlight.Server.BussinessCore.Product
{
    public class BLItinerary
    {
        private readonly DLItinerary _dLItinerary;
        private readonly DLAirline _dLAirline;
        public BLItinerary()
        {
            _dLItinerary = new DLItinerary();
            _dLAirline = new DLAirline();
        }
        public ItineraryDTO GetItineraryDetails(ItineraryDTO itineraryDTO)
        {
            if (itineraryDTO != null)
            {
                ItineraryDTO itineraryDetails = _dLItinerary.GetItineraryList(itineraryDTO);
                if (itineraryDetails != null)
                {
                    var AllAirlineLogos = _dLAirline.GetAllAirlineLogo();
                    // for (int i = 0; i < itineraryDetails?.AirSegments?.Count; i++)
                    // {
                    itineraryDetails?.AirSegments?.ForEach(z =>
                    {
                        z.AirlineLogoUrl = (AllAirlineLogos?.Where(y => y.AirlineCode == z.Carrier)?.FirstOrDefault()?.AirlineLogo ?? "").Replace("http://80.194.77.147/BSAdminPanel/images", "https://cms.brightsun.co.uk/images/Airlineimg");
                    });
                    // itineraryDetails.AirSegments[i].AirlineLogoUrl = AllAirlineLogos?.Where(y => y.AirlineCode == itineraryDetails.AirSegments[i].Carrier)?.FirstOrDefault()?.AirlineLogo ?? "";
                    // }
                }
                return itineraryDetails ?? new();
            }
            return new ItineraryDTO();
        }
        public List<ItineraryDTO> SubmitItitnerary(ItineraryDTO itineraryDTO)
        {
            return _dLItinerary.SubmitItinerary(itineraryDTO);
        }
    }
}
