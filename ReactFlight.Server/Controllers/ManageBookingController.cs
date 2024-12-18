using Microsoft.AspNetCore.Mvc;
using ReactFlight.Server.BussinessCore.Product;
using ReactFlight.Server.Model.Product;
namespace ReactFlight.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManageBookingController : ControllerBase
    {
        [HttpPost]
        [Route("[action]")]
        public IActionResult ManageBookingRef([FromBody] BookingDTO model)
        {
            ItineraryDTO itineraryDTO = new();
            itineraryDTO.BookingRef = model.BookingRef;
            itineraryDTO.PaxInfos.Add(new Model.PaxDTO
            {
                LastName = model.LastName,
               // IsLeadName = false,
            });
            itineraryDTO = new BLItinerary().GetItineraryDetails(itineraryDTO) ?? new();
            if (itineraryDTO == null)
            {
                NotFound(new { error = "Error", message = "fetching error in response" });
            }
            return Ok(new { success = "OK", data = itineraryDTO });
        }
    }
    public class BookingDTO
    {
        public string BookingRef { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
    }
}
