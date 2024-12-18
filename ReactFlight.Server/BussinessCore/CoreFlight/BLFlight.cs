using Newtonsoft.Json;
using ReactFlight.Server.BussinessCore.AbstractFlight;
using ReactFlight.Server.BussinessCore.Common;
using ReactFlight.Server.BussinessCore.Product.Flight.BST;
using ReactFlight.Server.Model;

namespace ReactFlight.Server.BussinessCore.CoreFlight
{
    public class BLFlight : ILFlight
    {
        private Credential Credential;
        private readonly IConfiguration Configuration;
        public BLFlight(IConfiguration Configuration)
        {
            this.Configuration = Configuration;
            Credential = new Credential();
        }
        public async Task<ResponseModel> FlightResponse(RequestModel requestModel)
        {
            ResponseModel? responseModel = new ResponseModel();
            requestModel.WebsiteName = Credential?.BST?.Websitename;
            requestModel.CompanyCode = Credential?.BST?.AccountCode;
            string request = BSTRequest.FlightSearchRequest(requestModel);
            BSTConnection connection = new(Configuration, Credential?.BST?.BaseUrl ?? "" ?? "", Credential?.BST?.FlightSearch ?? "", MyEnum.Method.POST.ToString());
            string? result = await connection.SendRequest(request);
            responseModel = JsonConvert.DeserializeObject<ResponseModel>(result);
            return responseModel ?? new();
        }
        public async Task<ResponseModel> PriceResponse(RequestModel requestModel)
        {
            ResponseModel? responseModel = new ResponseModel();
            requestModel.WebsiteName = Credential?.BST?.Websitename;
            requestModel.CompanyCode = Credential?.BST?.AccountCode;
            requestModel.AccountCode = Credential?.BST?.AccountCode;
            string request = BSTRequest.PricingRequest(requestModel);
            BSTConnection connection = new(Configuration, Credential?.BST?.BaseUrl ?? "" ?? "", Credential?.BST?.FlightPrice ?? "", MyEnum.Method.POST.ToString());
            string? result = await connection.SendRequest(request);
            responseModel = JsonConvert.DeserializeObject<ResponseModel>(result);
            return responseModel ?? new();
        }

    }
}
