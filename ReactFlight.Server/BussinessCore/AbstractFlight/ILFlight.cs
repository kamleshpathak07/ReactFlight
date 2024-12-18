using ReactFlight.Server.Model;

namespace ReactFlight.Server.BussinessCore.AbstractFlight
{
    public interface ILFlight
    {
        public Task<ResponseModel> FlightResponse(RequestModel requestModel);
        public Task<ResponseModel> PriceResponse(RequestModel requestModel);
    }
}
