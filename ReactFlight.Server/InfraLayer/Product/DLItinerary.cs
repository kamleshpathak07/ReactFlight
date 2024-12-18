using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using ReactFlight.Server.BussinessCore.Common;
using ReactFlight.Server.BussinessCore.Product;
using ReactFlight.Server.InfraLayer.DATA;
using ReactFlight.Server.InfraLayer.DataRepository;
using ReactFlight.Server.Model;
using ReactFlight.Server.Model.Common;
using ReactFlight.Server.Model.Miscellaneous;
using ReactFlight.Server.Model.Product;
using ReactFlight.Server.Model.Product.Flight;
using ReactFlight.Server.Model.User;
using System.Data;
using TSHAK.Components;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ReactFlight.Server.InfraLayer.Product
{
    public class DLItinerary : IDisposable
    {
        private readonly DataContext _dataContext;
        private bool _disposed;
        public DLItinerary()
        {
            _dataContext = new DataContext();
        }
        public List<ItineraryDTO> SubmitItinerary(ItineraryDTO itineraryDTO)
        {
            string errorDescription = string.Empty;
            using (IBEEntity? iBEEntity = (IBEEntity?)_dataContext.GetDBContext(MYEnum.Database.IBE))
            {
                try
                {
                    #region Submit Itinerary Into Master and Booking Details Table 
                    var parameteres = CreateItinerary(itineraryDTO);
                    string query = GetProcedureQuery(parameteres, "sp_IBE_NewFlightBooking");
                    var results = iBEEntity?.TransactionStatus.FromSqlRaw(query, parameteres.ToArray()).ToList().Select(transaction => new TransactionStatus
                    {
                        DESCRIPITON = transaction.DESCRIPITON,
                        INSERT_STATUS = transaction.INSERT_STATUS,
                    }).FirstOrDefault();
                    if ((results?.INSERT_STATUS ?? "") != MYEnum.DBRowChangeStatus.SUCCESS.ToString())
                    {
                        errorDescription = "Problem in Inserting the Itinearary into Booking Master and Booking Details Table";
                        goto SUBMITFAILED;
                    }
                    #endregion
                    #region INSERT PAX Info 
                    string paxInsertionQuery = "Exec IBE_PAXDetail_Insert_New_LIVE @paramPaxDetails";
                    var dt = TableCreation.GetTableForPaxDetails(itineraryDTO);
                    var pDt = new SqlParameter("@paramPaxDetails", SqlDbType.Structured);
                    pDt.Value = dt;
                    pDt.TypeName = "dbo.Passenger_Detail_New_LIVE";
                    var paxResult = iBEEntity?.Database.ExecuteSqlRaw(paxInsertionQuery, pDt);
                    #endregion
                    #region INSERT Segment Details 
                    string segmentInsertionQuery = "Exec IBE_TravelDetails_Insert_New @paramFlightDetails";
                    var segmentdt = TableCreation.GetTableForFlightDetails(itineraryDTO);
                    var segmentpDt = new SqlParameter("@paramFlightDetails", SqlDbType.Structured);
                    segmentpDt.Value = segmentdt;
                    segmentpDt.TypeName = "dbo.TravelDetails";
                    var segmentResult = iBEEntity?.Database.ExecuteSqlRaw(segmentInsertionQuery, segmentpDt);
                    #endregion
                    return new List<ItineraryDTO>()
                                    {
                                        new ItineraryDTO
                                        {
                                            ErrorInfo = new ErrorLogDTO{
                                                LogDescription = errorDescription,
                                                LogStatus = MYEnum.DBRowChangeStatus.SUCCESS.ToString(),
                                            }
                                        }
                                    };
                }
                catch (Exception ex)
                {
                    string message = $"{ex.Message} - {ex.StackTrace}";
                    errorDescription = ex.Message;
                    goto SUBMITFAILED;
                }
            }
        SUBMITFAILED:
            return new List<ItineraryDTO>()
                        {
                            new ItineraryDTO
                            {
                                ErrorInfo = new ErrorLogDTO{
                                    LogDescription = errorDescription,
                                    LogStatus = MYEnum.DBRowChangeStatus.ERROR.ToString(),
                                }
                            }
                        };
        }
        public ItineraryDTO GetItineraryList(ItineraryDTO objItineraryDTO)
        {
            List<ItineraryDTO> ititneraryList = GetItineraryByParams(objItineraryDTO);
            if (ititneraryList is List<ItineraryDTO> ARFItitneraryList)
            {
                List<PaxDTO> paxDTOs = new();
                List<AirSegmentInfo> airSegmentInfos = new List<AirSegmentInfo>();
                var itineraryDTOs = ARFItitneraryList.GroupBy(z => z.ProdRef);

                foreach (var group in itineraryDTOs)
                {
                    var ARFItinerary = group.ToList();
                    var firstItinerary = ARFItinerary.FirstOrDefault();
                    if (firstItinerary != null)
                    {
                        firstItinerary.AirSegments = RenderSegmentInfo(ARFItinerary);
                        firstItinerary.PaxInfos = RenderPaxInfo(ARFItinerary);
                        return firstItinerary;
                    }
                }
            }
            return new ItineraryDTO();
        }
        public List<ItineraryDTO> GetItineraryByParams(ItineraryDTO objItineraryDTO)
        {
            using (IBEEntity? iBEEntity = (IBEEntity?)_dataContext.GetDBContext(MYEnum.Database.IBE))
            {
                try
                {
                    List<SqlParameter> parameters = new()
                    {
                        new SqlParameter("@paramBookingRef", objItineraryDTO.BookingRef??(object)DBNull.Value ),
                        new SqlParameter("@paramPNR", objItineraryDTO.PNRInfo?.RecLoc??(object)DBNull.Value ),
                        new SqlParameter("@paramFName", objItineraryDTO.PaxInfos ?[0] ?.FirstName ??(object) DBNull.Value),
                        new SqlParameter("@paramLName", objItineraryDTO.PaxInfos ?[0] ?.LastName??(object)DBNull.Value ),
                        new SqlParameter("@paramIsLead", objItineraryDTO.PaxInfos ?[0] ?.IsLeadName??(object)DBNull.Value ),
                        new SqlParameter("@paramBookingBy", objItineraryDTO.AccountInfo ?.AccountCode??(object)DBNull.Value ),
                        new SqlParameter("@paramBookingMedia", objItineraryDTO.BookingMedia??(object)DBNull.Value ),
                        new SqlParameter("@paramBookingProdRef", objItineraryDTO.ProdRef??(object)DBNull.Value ),
                        new SqlParameter("@paramBookingStatus", objItineraryDTO.BookingStatus??(object)DBNull.Value ),
                        new SqlParameter("@paramFromBooked", objItineraryDTO.BookingDateRange.ContainsKey("From") ? objItineraryDTO.BookingDateRange["From"] : (object) DBNull.Value),
                        new SqlParameter("@paramToBooked", objItineraryDTO.BookingDateRange.ContainsKey("To") ? objItineraryDTO.BookingDateRange["To"] ??(object) DBNull.Value : (object) DBNull.Value),
                        new SqlParameter("@paramByCompany", objItineraryDTO.AccountInfo ?.AccountCode??(object)DBNull.Value ),
                        new SqlParameter("@paramOptrId", objItineraryDTO.PaxInfos ?[0].UserID??(object)DBNull.Value ),
                        new SqlParameter("@paramSupplier" ?? "", objItineraryDTO.SupplierRef??(object)DBNull.Value ),
                        new SqlParameter("@paramBookingByType", objItineraryDTO.AccountInfo ?.AccountType??(object)DBNull.Value ),
                        new SqlParameter("@paramFromDepart" ?? "", objItineraryDTO.DepartDateRange.ContainsKey("From") ? objItineraryDTO.DepartDateRange["From"] :(object) DBNull.Value),
                        new SqlParameter("@paramToDepart", objItineraryDTO.DepartDateRange.ContainsKey("To") ? objItineraryDTO.DepartDateRange["To" ?? ""] : (object) DBNull.Value),
                        new SqlParameter("@paramSegmentId", objItineraryDTO.AirSegments?[0].SegmentId??(object)DBNull.Value ),
                        new SqlParameter("@paramAirlineRef", (object)DBNull.Value ),
                    };
                    string sqlQuery = @"Exec sp_IBE_FlightSearch_Booking @paramBookingRef,@paramPNR,@paramFName,@paramLName,@paramIsLead,@paramBookingBy,@paramBookingMedia,@paramBookingProdRef,@paramBookingStatus,@paramFromBooked,@paramToBooked,@paramByCompany,@paramOptrId,@paramSupplier,@paramBookingByType,@paramFromDepart,@paramToDepart,@paramSegmentId,@paramAirlineRef";
                    var results = iBEEntity?.ProductSearchBookingDetails.FromSqlRaw(sqlQuery, parameters.ToArray()).ToList().Select
                                        (result => new ItineraryDTO
                                        {
                                            ProdBookingId = result.Prod_Booking_ID,
                                            ProdRef = result.ProdRef,
                                            SupplierRef = result.Supplier_Ref,
                                            BookingRef = result.Booking_Ref ?? "",
                                            BookingStatus = result.Booking_Status,
                                            BookingDate = Convert.ToString(result.Booking_Date_Time),
                                            BookingMedia = result.Booking_Media,
                                            BookingStatusDate = Convert.ToString(result.Status_Date),
                                            ProductStatus = result.Prod_Booking_Status,
                                            IsClearedFromQC = result.IsClearedQC,
                                            MetaSearch = result.Meta_Search,
                                            IsBreakDownShow = result.IsBreakDownShow,
                                            PaymentDate = result.PaymentDate,
                                            PaymentRemarks = result.PaymentRemark,
                                            Booking_Approval_Status = result.Approval_Status,
                                            SecureString = new SecureQueryString()
                                              {
                                              { "BookingRef", result.Booking_Ref },
                                              { "BookingDate", Convert.ToDateTime(result.Booking_Date_Time).ToString("dd/MM/yyyy") },
                                              { "productRef", result.ProdRef },
                                              { "ProdBookingId", result.Prod_Booking_ID },
                                              { "BookingMedia", result.Booking_Media },
                                              { "SupplierRef", result.Supplier_Ref },
                                              { "AccountCode", result.Booking_By },
                                              { "CompanyCode", result.Company },
                                                //{ "MailInTicketing", objItineraryDTO.MailSource }
                                              }.ToString(),
                                            AccountInfo = new AccountDTO
                                            {
                                                AccountCode = result.Booking_By,
                                                AccountType = result.Booking_By_Type,
                                                CompanyName = result.Company_Name,
                                                CompanyEmail = result.Company_Email,
                                                CompanyCode = result.Company
                                            },
                                            //PricingInfo = new PricingDTO { TotalPrice = Convert.ToDecimal(result.Total_Amount) },
                                            Currency = result.Currency,
                                            AgentRef = result.AgentRef,
                                            AdvertMedia = result.Advert_Media_Code,
                                            PNRInfo = new PNRInfoDTO
                                            {
                                                CreationDate = !string.IsNullOrEmpty(Convert.ToString(result.Booking_Date_Time)) ? ((DateTime)result.Booking_Date_Time).ToString("ddd, dd MMM yyyy HH:mm") : string.Empty,
                                                RecLoc = result?.PNR_Confirmation,
                                                UniversalRecLoc = result?.Universal_Record_Locator,
                                                //AirlineRef = GetAirlineRef(objItineraryDTO)?[0]?.PNRInfo?.AirlineRef//string.Empty
                                            },
                                            AirSegments = new List<AirSegmentInfo>()
                                            {
                                                new AirSegmentInfo()
                                                {
                                                    SegmentId = Convert.ToInt32(result?.Seg_Id??"0"),
                                                    MainOrigin = result ?.Origin,
                                                    MainDestination = result ?.Destination,
                                                    Origin = result ?.FromDestination,
                                                    Destination = result ?.ToDestination,
                                                    DepartDatetime = Convert.ToString(result?.Depart_Date),
                                                    DepartTime= Convert.ToString(result ?.Depart_Date),
                                                    DepartDate= Convert.ToString(result ?.Depart_Date),
                                                    AirportTerminal = result?.Arpttrminal,
                                                    BaggageDetails = result ?.Baggageallownce,
                                                    FareBasis=result ?.Farebasis,
                                                    //AirportTerminal=result.Arpttrminal,
                                                   // BaggageDetails=result.Baggageallownce,
                                                    ArrivalDatetime = Convert.ToString(result ?.Arrival_Date),
                                                    Carrier = result ?.CarierCode,
                                                    FlightNumber = result ?.FlightNo,
                                                    SubClass = result ?.Class,
                                                    Status = result ?.SegmentStatus,
                                                    SegmentRemarks = result ?.SegRemarks,
                                                    IsSegRemarkVisibleToCust = result?.IsSegRemarkVisibleToCust??false,
                                                    ChangeOfPlane =Convert.ToString(result?.ChangeOfPlane??false),
                                                    OperatingCarrier = result?.OperatingCarrier??"",
                                                    OperatingFlightNumber = result?.OperatingFlightNo,
                                                    NoOfStops = result?.NumberOfStops??0
                                                }
                                            },
                                            PaxInfos = new List<PaxDTO>()
                                            {
                                                new PaxDTO()
                                                {
                                                    UserID = result?.Operator_ID,
                                                    FirstName = result?.First_Name ?? string.Empty,
                                                    LastName = result?.Last_Name ?? string.Empty,
                                                    //MiddelName = result.Middle_Name,
                                                    IsLeadName = (bool)(result?.IsLeadPax??false),
                                                    PaxType = result ?.PaxType ?? string.Empty,
                                                    PaxId = Convert.ToInt32(!string.IsNullOrEmpty(result?.Pax_ID?.Trim()??"")?result?.Pax_ID??"0":"0"),
                                                    Gender = result ?.Gender??string.Empty,
                                                    PassportNo = result ?.Passport_No,
                                                    PaxDOB = result ?.DOB,
                                                    PCR_TestDateTime = result?.PCR_TestDateTime == null? DateTime.Parse("01/01/1900") : result ?.PCR_TestDateTime.Value
                                                }
                                            }
                                        }).ToList();
                    return results ?? new();
                }
                catch (Exception ex)
                {
                    string message = $"{ex.Message}-{ex.StackTrace}";
                }
            }
            return new List<ItineraryDTO>();
        }
        public List<AirSegmentInfo> RenderSegmentInfo(List<ItineraryDTO> objListItineraryDTO)
        {
            List<AirSegmentInfo> segmentInfos = new List<AirSegmentInfo>();
            if (objListItineraryDTO != null)
            {
                if (objListItineraryDTO.Count > 0)
                {
                    List<int> segIds = new List<int>();
                    List<AirSegmentInfo> newSegmentInfos = new List<AirSegmentInfo>();
                    newSegmentInfos.AddRange(objListItineraryDTO.Where(itinerary =>
                                                                itinerary.AirSegments?.Count > 0 && (itinerary.ProdRef == MyEnum.Product.ARF.ToString() || itinerary.ProdRef == MyEnum.Product.INS.ToString()) &&
                                                                (int)itinerary.AirSegments[0].SegmentId != 0 && !segIds.Contains((int)itinerary.AirSegments[0].SegmentId))
                                                                .Select(itinerary => { segIds.Add((int)(itinerary.AirSegments?[0]?.SegmentId ?? 0)); return (itinerary.AirSegments?[0] ?? new()); })
                                                                 );
                    segmentInfos.AddRange(newSegmentInfos.Select(segment =>
                    {
                        return new AirSegmentInfo
                        {
                            SegmentId = segment.SegmentId,
                            MainOrigin = segment.MainOrigin,
                            MainDestination = segment.MainDestination,
                            Origin = segment.Origin,
                            DepartDatetime = Convert.ToDateTime(segment.DepartDatetime).ToString("ddd, dd MMM yyyy HH:mm"),
                            DepartTime = Convert.ToDateTime(segment.DepartDatetime).ToString("ddd, dd MMM yyyy HH:mm"),
                            DepartDate = Convert.ToDateTime(segment.DepartDatetime).ToString("dd/MM/yyyy"),
                            Destination = segment.Destination,// AirportDetails(segment.Destination);
                            ArrivalDatetime = DateTime.Parse(segment.ArrivalDatetime?.ToString() ?? DateTime.MinValue.ToString()).ToString("ddd, dd MMM yyyy HH:mm"),
                            ArrivalDate = DateTime.Parse(segment.ArrivalDatetime?.ToString() ?? DateTime.MinValue.ToString()).ToString("dd/MM/yyyy"),
                            AirportTerminal = segment.AirportTerminal,
                            BaggageDetails = segment.BaggageDetails,
                            FareBasis = segment.FareBasis,
                            FlightNumber = segment.FlightNumber,
                            AirlineLogoUrl = segment.AirlineLogoUrl,
                            Carrier = segment.Carrier,
                            //CarrierName = airlineDetails.AirlineName,
                            //AirecraftName = airlineDetails.AirlineCode + segment.FlightNumber,
                            //AirlineCode = segment.Carrier,
                            Class = segment.SubClass,
                            OperatingCarrier = !string.IsNullOrEmpty(segment.ChangeOfPlane) || !string.IsNullOrEmpty(segment.OperatingCarrier) ? (segment.ChangeOfPlane ?? "").Equals("TRUE", StringComparison.OrdinalIgnoreCase) ? !string.IsNullOrEmpty(segment.OperatingCarrier) ? segment.OperatingCarrier : "\"*&nbsp;There is a change of plane on this flight segment.\"" : "" : "",
                            NoOfStops = segment.NoOfStops,
                            ChangeOfPlane = segment.ChangeOfPlane,
                            IsSegRemarkVisibleToCust = segment.IsSegRemarkVisibleToCust,
                            SegmentRemarks = segment.SegmentRemarks,
                        };
                    }));
                }
            }
            return segmentInfos;
        }
        public List<PaxDTO> RenderPaxInfo(List<ItineraryDTO> objListItineraryDTO)
        {
            List<PaxDTO> returnUserInfos = new List<PaxDTO>();
            List<int> paxIds = new List<int>();
            List<PaxDTO> newUserInfos = new List<PaxDTO>();
            if (objListItineraryDTO != null)
            {
                if (objListItineraryDTO.Count > 0)
                {
                    ItineraryDTO objItineraryDTO = new ItineraryDTO();
                    objItineraryDTO.BookingRef = objListItineraryDTO[0].BookingRef;
                    objItineraryDTO.ProdBookingId = null;
                    newUserInfos.AddRange(objListItineraryDTO.SelectMany(z => z.PaxInfos.Where(y => !newUserInfos.Any(pax => pax.PaxId == y.PaxId)).Select(p => new PaxDTO
                    {
                        PaxId = p.PaxId,
                        FirstName = p.FirstName,
                        LastName = p.LastName,
                        PaxDOB = p.PaxDOB,
                        Title = p.Title,
                        PaxType = p.PaxType,
                        Email = p.Email,
                        MobileNumber = p.MobileNumber,
                        CompCode = p.CompCode,
                        ContactNo = p.ContactNo,
                    })));
                }
            }
            if (newUserInfos != null)
            {
                if (newUserInfos.Count > 0)
                {
                    foreach (PaxDTO user in newUserInfos)
                    {
                        PaxDTO objUserDTO = new PaxDTO();
                        objUserDTO.PaxId = user.PaxId;
                        objUserDTO.PaxType = user.PaxType;
                        objUserDTO.LastName = user.LastName;
                        objUserDTO.FirstName = user.FirstName;
                        objUserDTO.IsLeadName = user.IsLeadName;
                        objUserDTO.Gender = user.Gender;
                        objUserDTO.PassportNo = user.PassportNo;
                        objUserDTO.PaxDOB = user.PaxDOB;
                        returnUserInfos.Add(objUserDTO);
                    }
                }
            }
            return returnUserInfos;
        }
        public List<PaxDTO> GetALLPax(ItineraryDTO itineraryDTO)
        {

            return new List<PaxDTO>();
        }
        private List<SqlParameter> CreateItinerary(ItineraryDTO objItinerary)
        {
            List<SqlParameter> sqlParameterList = new List<SqlParameter>() {
                                                        new SqlParameter("@paramBookingRef", SqlDbType.NVarChar, 50) { Value = objItinerary?.BookingRef??(object)DBNull.Value},
                                                        new SqlParameter("@paramInvoiceNo", SqlDbType.NVarChar, 50) { Value = objItinerary?.BookingRef ?? (object)DBNull.Value },
                                                        new SqlParameter("@paramOrigin", SqlDbType.NVarChar, 50) { Value = objItinerary?.AirSegments?[0].Origin?.ToUpper() ?? (object)DBNull.Value },
                                                        new SqlParameter("@paramDestination", SqlDbType.NVarChar, 50) { Value = objItinerary?.Destination ?? (object)DBNull.Value },
                                                        new SqlParameter("@paramBookingStatus", SqlDbType.NVarChar, 100) { Value = objItinerary?.BookingStatus ?? (object)DBNull.Value },
                                                        new SqlParameter("@paramTotBokAmt", SqlDbType.Money) { Value = objItinerary?.PricingInfo?.TotalPrice ?? (object)DBNull.Value },
                                                        new SqlParameter("@paramCurrency", SqlDbType.NVarChar, 25) { Value = MyEnum.Currency.GBP.ToString() },
                                                        new SqlParameter("@paramBooking_By_Company", SqlDbType.NVarChar, 50) { Value = objItinerary?.AccountInfo?.CompanyCode ?? (object)DBNull.Value },
                                                        new SqlParameter("@paramAgntRef", SqlDbType.NVarChar, 50) { Value = objItinerary?.AgentRef ?? (object)DBNull.Value },
                                                        new SqlParameter("@paramLogId", SqlDbType.NVarChar, 50) { Value = objItinerary?.LogInfo?.LogId ?? (object)DBNull.Value },
                                                        new SqlParameter("@paramAccessedStatus", SqlDbType.Bit) { Value = true },
                                                        new SqlParameter("@paramPromocode", SqlDbType.NVarChar, 50) { Value = (object)DBNull.Value },

                                                        new SqlParameter("@paramBookingStatusDate", SqlDbType.DateTime) { Value = DateTime.Now },
                                                        new SqlParameter("@paramUniversalRecordLocator", SqlDbType.NVarChar, 10) { Value = objItinerary?.PNRInfo?.RecLoc ?? (object)DBNull.Value },
                                                        new SqlParameter("@paramAuditID", SqlDbType.NVarChar, 50) { Value = objItinerary?.AuditID ?? (object)DBNull.Value },
                                                        new SqlParameter("@paramMetaSearch", SqlDbType.NVarChar, 50) { Value = objItinerary?.MetaSearch ?? string.Empty },
                                                        new SqlParameter("@paramIsCleared_FromQC", SqlDbType.Bit) { Value = objItinerary?.IsClearedFromQC ?? false },
                                                        new SqlParameter("@paramIs_Group_Booking", SqlDbType.Bit) { Value = true },
                                                        new SqlParameter("@paramBookingType", SqlDbType.NVarChar, 50) { Value = objItinerary?.BookingType ?? (object)DBNull.Value },
                                                        new SqlParameter("@@ParamCopyorReissue", SqlDbType.Bit) { Value = (object)DBNull.Value },
                                                        new SqlParameter("@paramProdRef", SqlDbType.NVarChar, 50) { Value = objItinerary?.ProdRef ?? (object)DBNull.Value },
                                                        new SqlParameter("@paramSupplierRef", SqlDbType.NVarChar, 50) { Value = objItinerary?.SupplierRef ?? (object)DBNull.Value },
                                                        new SqlParameter("@paramProdBookingId", SqlDbType.NVarChar, 50) { Value = objItinerary?.ProdBookingId ?? (object)DBNull.Value },
                                                        new SqlParameter("@paramBookingBy", SqlDbType.NVarChar, 100) { Value = objItinerary?.AccountInfo?.AccountCode ?? (object)DBNull.Value },
                                                        new SqlParameter("@paramBookingByType", SqlDbType.NVarChar, 100) { Value = objItinerary?.AccountInfo?.AccountType ?? (object)DBNull.Value },
                                                        new SqlParameter("@paramBookingMedia", SqlDbType.NVarChar, 100) { Value = objItinerary?.BookingMedia ?? (object)DBNull.Value },
                                                        new SqlParameter("@paramBookingDatenTime", SqlDbType.DateTime) { Value = DateTime.Now },
                                                        new SqlParameter("@paramProductStatus", SqlDbType.NVarChar, 100) { Value = objItinerary?.ProductStatus ?? (object)DBNull.Value },
                                                        new SqlParameter("@paramProdAmt", SqlDbType.Money) { Value = objItinerary?.PricingInfo?.TotalPrice ?? (object)DBNull.Value },
                                                        new SqlParameter("@paramProductPnr", SqlDbType.NVarChar, 50) { Value = objItinerary?.PNRInfo?.RecLoc ?? (object)DBNull.Value },
                                                        new SqlParameter("@paramSuppBkingRef", SqlDbType.NVarChar, 50) { Value = objItinerary?.SupplierRef ?? (object)DBNull.Value },
                                                        new SqlParameter("@paramAdvertMedia", SqlDbType.NVarChar, 50) { Value = objItinerary?.AdvertMedia ?? (object)DBNull.Value },
                                                        new SqlParameter("@paramOptrId", SqlDbType.NVarChar, 100) { Value = objItinerary?.AccountInfo?.UserInfo?.UserID ?? (object)DBNull.Value },
                                                        new SqlParameter("@@paramAdhoc", SqlDbType.NVarChar, 100) { Value = string.Empty },
                                                        new SqlParameter("@paramJourneyType", SqlDbType.NVarChar, 100) { Value = "N/A" },
                                                        new SqlParameter("@paramEndorsRestrict", SqlDbType.NVarChar, 100) { Value = "N/A" },
                                                        new SqlParameter("@paramAirlinesData", SqlDbType.NVarChar, 200) { Value = "N/A" },
                                                        new SqlParameter("@paramExchangeFor", SqlDbType.NVarChar, 100) { Value = "N/A" },
                                                        new SqlParameter("@paramTicketArrange", SqlDbType.NVarChar, 100) { Value = objItinerary?.BookingRef ?? (object)DBNull.Value },
                                                        new SqlParameter("@paramTicketRemarks", SqlDbType.NVarChar, 1000) { Value = objItinerary?.BookingRef ?? (object)DBNull.Value },
                                                        new SqlParameter("@paramAirRef", SqlDbType.NVarChar, 50) { Value = objItinerary?.AirlineRef ?? (object)DBNull.Value },
                                                        new SqlParameter("@paramPCC", SqlDbType.NVarChar, 15) { Value = objItinerary?.PNRInfo?.AgencyPCC ?? (object)DBNull.Value },
                                                        new SqlParameter("@AgentSignOn", SqlDbType.NVarChar, 25) { Value = objItinerary?.PNRInfo?.AgentSignon ?? (object)DBNull.Value },
                                                        new SqlParameter("@AgencyIATA", SqlDbType.NVarChar, 25) { Value = objItinerary?.PNRInfo?.AgencyIATANum ?? (object)DBNull.Value },
                                                        new SqlParameter("@OriginalReceived", SqlDbType.NVarChar, 50) { Value = objItinerary?.PNRInfo?.OriginalReceivedFieldValue ?? (object)DBNull.Value },
                                                        new SqlParameter("@TransitPoint", SqlDbType.VarChar, 200) { Value = "N/A" }
            };
            return sqlParameterList;
        }
        private string GetProcedureQuery(List<SqlParameter> sqlParameterList, string procedureName)
        {
            List<string> parameters = sqlParameterList.Select(z => z.ParameterName).ToList();
            string query = $"Exec {procedureName} {string.Join(",", parameters)}";
            return query;
        }
        public void Dispose()
        {
            if (_disposed)
            {
                //_dataContext = null;
            }
            return;
        }
        public void Dispose(bool dispose)
        {
            if (dispose)
                return;
        }
        ~DLItinerary()
        {
            Dispose();
        }
    }
}
