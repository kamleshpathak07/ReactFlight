using Microsoft.AspNetCore.Mvc;
using ReactFlight.Server.BussinessCore.AbstractFlight;
using ReactFlight.Server.BussinessCore.Common;
using ReactFlight.Server.Model;
using ReactFlight.Server.Model.Product;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace ReactFlight.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        private readonly ILFlight _iLFlight;
        private readonly ILAirport _airport;
        private readonly IHttpContextAccessor _contextAccessor;
        public FlightController(ILFlight lFlight, ILAirport airport, IHttpContextAccessor contextAccessor)
        {
            _iLFlight = lFlight;
            _airport = airport;
            _contextAccessor = contextAccessor;
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> FetchFlight([FromBody] RequestModel requestModel)
        {
            ResponseModel responseModel = null;
            string ExtractValueInBrackets(string input)
            {
                Match match = Regex.Match(input, @"\[(.*?)\]");
                return match.Success ? match.Groups[1].Value : input;
            }
            requestModel.Origin = ExtractValueInBrackets(requestModel?.Origin ?? "");
            requestModel.Destination = ExtractValueInBrackets(requestModel?.Destination ?? "");
            try
            {
                responseModel = await _iLFlight.FlightResponse(requestModel);
                if (responseModel == null)
                {
                    NotFound(new { error = "Error", message = "fetching error in response" });
                }
            }
            catch (Exception ex)
            {
                return NotFound(new { error = "Error", message = "fetching error in response" });
            }
            return Ok(new { success = "OK", data = responseModel });
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> FlightPrice([FromBody] RequestModel requestModel)
        {
            ResponseModel responseModel = null;
            responseModel = await _iLFlight.PriceResponse(requestModel);
            if (responseModel == null)
            {
                NotFound(new { error = "Error", message = "fetching error in response" });
            }
            return Ok(new { success = "OK", data = responseModel });
        }
        [HttpPost]
        [Route("[action]")]
        public IActionResult Autocomplete([FromBody] AutoCompleteReq autoCompleteReq)
        {
            var result = _airport.GET_AirportAutoComplete(autoCompleteReq.prefixtext);
            if (result == null)
            {
                NotFound(new { error = "Error", message = "fetching error in response" });
            }
            return Ok(new { success = "OK", data = result });
        }
        [HttpPost]
        [Route("[action]")]
        [BookingActionFilter]
        public Task<IActionResult> FlightPnr([FromBody] RequestModel requestModel)
        {
            string name = requestModel.PaxInfoList?.FirstOrDefault()?.FirstName ?? string.Empty;
            string lastName = requestModel.PaxInfoList?.FirstOrDefault()?.LastName ?? string.Empty;
            string DOB = Convert.ToDateTime(requestModel.PaxInfoList?.FirstOrDefault()?.PaxDOB ?? MyEnum.CustomDateTime.DefaultDateTime).ToString("dd/MM/yyyy");
            var itineraryDetails = new ItineraryDTO();
            var jsonDetails = HttpContext.Items["ItineraryDetail"] as string;
            itineraryDetails = jsonDetails != null ? JsonSerializer.Deserialize<ItineraryDTO>(jsonDetails ?? string.Empty) : new ItineraryDTO();
            return Task.FromResult<IActionResult>(Ok(new { success = "OK", itineraryDetail = itineraryDetails }));
        }
        public class AutoCompleteReq
        {
            public string? product { get; set; }
            public string? prefixtext { get; set; }
        }
    }
}
