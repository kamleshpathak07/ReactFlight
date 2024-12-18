using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
//using Newtonsoft.Json;
using ReactFlight.Server.BussinessCore.Common;
using ReactFlight.Server.BussinessCore.Product;
using ReactFlight.Server.BussinessCore.User;
using ReactFlight.Server.Model;
using ReactFlight.Server.Model.Miscellaneous;
using ReactFlight.Server.Model.Product;
using ReactFlight.Server.Model.Product.Flight;
using ReactFlight.Server.Model.User;
using System.Text.Json;

namespace ReactFlight.Server.Controllers
{
    public class BookingActionFilter : ActionFilterAttribute, IActionFilter
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var httpContext = filterContext.HttpContext.RequestServices.GetService(typeof(IHttpContextAccessor)) as IHttpContextAccessor;
            var itineraryDTO = new ItineraryDTO();
            var responseModelInfo = filterContext.ActionArguments["requestModel"] as RequestModel;
            itineraryDTO.AirSegments = new List<AirSegmentInfo>();
            itineraryDTO.PaxInfos = responseModelInfo?.PaxInfoList as List<PaxDTO> ?? new List<PaxDTO>();
            if ((responseModelInfo?.PaxInfoList?.Count ?? 0) > 0)
                itineraryDTO.PaxInfos[0].IsLeadName = true;
            itineraryDTO.AirSegments = GetSegmentList(responseModelInfo ?? new());
            itineraryDTO.Email = itineraryDTO.PaxInfos?[0]?.ContactNo ?? string.Empty;
            itineraryDTO.BookingRef = new BLGenrateID().GetBookingRefIds(MyEnum.BookingPreFix.IBE.ToString());
            itineraryDTO.Origin = responseModelInfo?.AirSolution?[0]?.Journey?.FirstOrDefault()?.AirSegments?.FirstOrDefault()?.Origin ?? "";
            itineraryDTO.Destination = responseModelInfo?.AirSolution?[0]?.Journey?.FirstOrDefault()?.AirSegments?.LastOrDefault()?.Destination ?? "";
            itineraryDTO.Supp = MyEnum.Supp.GAL.ToString();
            itineraryDTO.AdvertMedia = MyEnum.CustomerType.DICT.ToString();
            itineraryDTO.BookingMedia = MyEnum.BookingMedia.TeleUk;
            itineraryDTO.AccountCode = MyEnum.AccountCode.DICTAccountCode;
            itineraryDTO.Currency = MyEnum.Currency.GBP.ToString();
            itineraryDTO.AgentRef = string.Empty;
            itineraryDTO.AirlineRef = string.Empty;
            itineraryDTO.BookingMedia = MyEnum.BookingMedia.BTpremier;
            itineraryDTO.BookingDate = DateTime.Now.ToString();
            itineraryDTO.Email = itineraryDTO.Email;
            itineraryDTO.ProdRef = MyEnum.Product.ARF.ToString();
            itineraryDTO.BookingStatus = MyEnum.Status.INCOMPLETE.ToString();
            itineraryDTO.SupplierRef = MyEnum.Supp.GAL.ToString();

            itineraryDTO.AuditID = itineraryDTO?.AuditID ?? string.Empty;
            itineraryDTO.ProdBookingId = "0001";
            itineraryDTO.PricingInfo = new PricingDTO()
            {
                TotalPrice = Convert.ToDecimal(responseModelInfo?.AirSolution?[0]?.TotalPrice ?? 0),
                BasePrice = Convert.ToDecimal(responseModelInfo?.AirSolution?[0]?.BasePrice ?? 0),
                Tax = Convert.ToDecimal(responseModelInfo?.AirSolution?[0]?.Tax ?? 0)
            };
            UserDTO userDTO = new UserDTO()
            {
                UserID = itineraryDTO?.PaxInfos?[0]?.Email ?? "",
                Title = itineraryDTO?.PaxInfos?[0].Title ?? "",
                FirstName = itineraryDTO?.PaxInfos?[0].FirstName ?? "",
                LastName = itineraryDTO?.PaxInfos?[0].LastName ?? "",
                Email = itineraryDTO?.PaxInfos?[0]?.Email ?? "",
                ContactNo = itineraryDTO?.PaxInfos?[0]?.ContactNo ?? "",
                LandlineNo = itineraryDTO?.PaxInfos?[0]?.ContactNo ?? "",
                CompCode = MyEnum.AccountCode.DICTAccountCode,
            };
            List<UserDTO> AccountInfo = new BLAccount().GetAccountInfo(userDTO);
            if (AccountInfo is List<UserDTO> user)
            {
                itineraryDTO.AccountInfo.AccountCode = AccountInfo?[0]?.AccountCode ?? string.Empty;
                itineraryDTO.AccountInfo.AccountType = AccountInfo?[0]?.AccessType ?? string.Empty;
                itineraryDTO.AccountInfo.UserInfo.UserID = user?[0]?.UserID ?? string.Empty;
                itineraryDTO.AccountInfo.CompanyCode = MyEnum.AccountCode.DICTAccountCode;
            }
            else
            {
                itineraryDTO.AccountInfo = RegisterAccountInfo(itineraryDTO)??new();
            }
            if (!string.IsNullOrEmpty(itineraryDTO.BookingMedia))
            {
                BLGenrateID bLGenrateID = new BLGenrateID();
                itineraryDTO.BookingRef = bLGenrateID.GetBookingRefIds(MyEnum.BookingPreFix.IBE.ToString());
                itineraryDTO.LogInfo = new Model.Miscellaneous.LogDetailDTO
                {
                    AccessedBy = itineraryDTO.Email,
                    AccessedByType = MyEnum.CustomerType.DICT.ToString(),
                    LogId = bLGenrateID.GetBookingRefIds(MyEnum.BookingPreFix.LOG.ToString()),
                    BookingRef = itineraryDTO.BookingRef,
                    AccessedIP = httpContext?.HttpContext?.Connection?.RemoteIpAddress?.MapToIPv4()?.ToString() ?? string.Empty,
                    LoginDateTime = DateTime.Now
                };
                itineraryDTO.TransactionInfo = new TransactionDTO
                {
                    TransactionNO = bLGenrateID.GetBookingRefIds(MyEnum.BookingPreFix.TRN.ToString()),
                    TransactionType = MyEnum.PaymentStatus.OPTION.ToString(),
                    PaymentStatus = MyEnum.PaymentStatus.DUE.ToString(),
                    TransactionAmount = 0.00M,
                    CurrencyType = MyEnum.Currency.GBP.ToString(),
                    BankingDateTime = DateTime.Now,
                    CardCharges = 0.00M,
                    InsuranceCharges = 50,
                    AncilliaryCharges = 50,
                    Discount = 0.00M,
                    CRDChargesType = "A",
                    TransactionMedia = "1"
                };
                #region Hold PNR Infos
                itineraryDTO.PNRInfo = new PNRInfoDTO
                {
                    UniversalRecLoc = string.Empty,
                    RecLoc = string.Empty,
                    AgencyPCC = string.Empty,
                    AgentSignon = string.Empty,
                    AgencyIATANum = string.Empty,
                    OriginalReceivedFieldValue = string.Empty
                };
                #endregion
            }
            var submitStatus = new BLItinerary().SubmitItitnerary(itineraryDTO);
            filterContext.HttpContext.Items["ItineraryDetail"] = JsonSerializer.Serialize(itineraryDTO);
        }
        private List<AirSegmentInfo> GetSegmentList(RequestModel responseModelInfo)
        {
            List<AirSegmentInfo> airSegmentInfos = new List<AirSegmentInfo>();
            foreach (var objsegment in responseModelInfo?.AirSolution?.FirstOrDefault()?.Journey?.SelectMany(z => z.AirSegments ?? new()).ToList() ?? new())
            {
                airSegmentInfos.Add(new AirSegmentInfo
                {
                    Origin = objsegment.Origin,
                    Destination = objsegment.Destination,
                    AirlineName = objsegment.AirlineName,
                    Airport = objsegment.Airport,
                    OriginAirportCity = objsegment.OriginAirportCity,
                    DestinationAirportCity = objsegment.DestinationAirportCity,
                    DepartDate = objsegment.DepartDate,
                    ArrivalDate = objsegment.ArrivalDate,
                    BaggageInfo = objsegment.BaggageInfo,
                    Carrier = objsegment.Carrier,
                    AirlineLogoUrl = objsegment.AirlineLogoUrl
                });
            }
            return airSegmentInfos;
        }
        private AccountDTO RegisterAccountInfo(ItineraryDTO itineraryDTO)
        {
            string strAccountCode = new BLGenrateID().GetBookingRefIds(MyEnum.Customer.DICT);
            Random r = new Random();
            string strACTIVATIONCODE = (r.Next(9999).ToString());
            strACTIVATIONCODE = (itineraryDTO.PaxInfos?[0]?.FirstName?.Substring(0, 1) + itineraryDTO.PaxInfos?[0]?.LastName?.Substring(0, 1)) + strACTIVATIONCODE;
            string strEncrptdPwd = TSHAK.Components.Hash.GetHash(strACTIVATIONCODE, TSHAK.Components.Hash.HashType.MD5);
            AccountDTO objRegUser = new AccountDTO
            {
                UserInfo = new UserDTO
                {
                    UserID = itineraryDTO.Email,
                    Title = itineraryDTO.PaxInfos?[0].Title,
                    FirstName = itineraryDTO.PaxInfos?[0].FirstName,
                    LastName = itineraryDTO.PaxInfos?[0].LastName,
                    Email = itineraryDTO.PaxInfos?[0].Email,
                    ContactNo = itineraryDTO.PaxInfos?[0].ContactNo,
                    LandlineNo = itineraryDTO.PaxInfos?[0].ContactNo
                },
                // LoginInfo = new LoginDTO { Password = strEncrptdPwd },
                AccountType = MyEnum.Customer.DICT,
                AccountCode = strAccountCode,
                CompanyCode = MyEnum.AccountCode.DICTAccountCode,
                /*Street = new StreetDTO
                {
                    HouseNo = string.Empty,// itineraryDTO.AccountInfo.Street.HouseNo,
                    Address1 = string.Empty,// itineraryDTO.AccountInfo.Street.Address1,
                    PostalCode = string.Empty
                },
                */
                City = new CityDTO { CityName = "" },
                Country = new CountryDTO() { CountryName = "" }
            };
            return objRegUser;
        }
    }
}
