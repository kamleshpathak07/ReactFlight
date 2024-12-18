using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ReactFlight.Server.BussinessCore.Common;
using ReactFlight.Server.InfraLayer.DATA;
using ReactFlight.Server.InfraLayer.DataRepository;
using ReactFlight.Server.Model.User;
using System.Data;

namespace ReactFlight.Server.InfraLayer.Product.User
{
    public class DLAccount : IDisposable
    {
        private DataContext _dataContext;
        private SHELLEntity _shellEntity;
        private IBEEntity _iBEEntity;
        private SHELLEntity _sHELLEntity;
        private bool _disposed;
        public DLAccount()
        {
            _dataContext = new DataContext();
        }
        public List<UserDTO> GetUserDetails(UserDTO userDTO)
        {
            List<UserDTO> userList = new List<UserDTO>();
            using (var brightSunEntity = (BrightsunEntity?)_dataContext.GetDBContext(MyEnum.Database.BRIGHTSUN))
            {
                try
                {
                    List<SqlParameter> parameters = new List<SqlParameter>()
                    {
                        new SqlParameter("@USERID", userDTO.UserID),
                        new SqlParameter("@CompCode",userDTO.CompCode)
                    };
                    var result = brightSunEntity?.userInformation_CompCode_UserIDs.FromSqlRaw("Exec sp_BS_APPSECMOD_User_Information_Get_UserDetail_By_UserID_CompCode @USERID,@CompCode", parameters.ToArray()).ToList().Select(user => new UserDTO()
                    {
                        AccountCode = user.BS_APPSECMOD_EXT_USR_CMP_DTL_Account_code,
                        CompCode = user.BS_APPSECMOD_EXT_USR_CMP_DTL_Company_code,
                        CompName = user.BS_APPSECMOD_EXT_USR_CMP_DTL_Company_Name,
                        BilCity = user.BS_APPSECMOD_USR_INF_City,
                        BilCountry = user.BS_APPSECMOD_USR_INF_Country,
                        BilHouseNo = user.BS_APPSECMOD_USR_INF_House_Number,
                        BilLphone = user.BS_APPSECMOD_USR_INF_Phone,
                        BilMob = user.BS_APPSECMOD_USR_INF_Mobile,
                        BilPcode = user.BS_APPSECMOD_USR_INF_Post_Code,
                        BilStreet = user.BS_APPSECMOD_USR_INF_Street,
                        Email = user.BS_APPSECMOD_USR_INF_EMail_Address,
                        AccessType = user.BS_APPSECMOD_USR_INF_AccessType,
                        FirstName = user.BS_APPSECMOD_USR_INF_FirstName,
                        LastName = user.BS_APPSECMOD_USR_INF_LastName,
                        FlightMarkupAP = user.BS_APPSECMOD_EXT_USR_CMP_DTL_FlightMarkup_A_P,
                        FlightMarkupAmt = user.BS_APPSECMOD_EXT_USR_CMP_DTL_Flight_Markup,
                        HotelMarkupAP = user.BS_APPSECMOD_EXT_USR_CMP_DTL_HotelMarkup_A_P,
                        HotelMarkupAmt = Convert.ToDecimal(user.BS_APPSECMOD_EXT_USR_CMP_DTL_Hotel_Markup),
                        InsuranceMarkupAP = user.BS_APPSECMOD_EXT_USR_CMP_DTL_InsuranceMarkup_A_P,
                        InsuranceMarkupAmt = user.BS_APPSECMOD_EXT_USR_CMP_DTL_Insurance_Markup,
                        CarMarkupAP = user.BS_APPSECMOD_EXT_USR_CMP_DTL_CarMarkup_A_P,
                        CarMarkupAmt = user.BS_APPSECMOD_EXT_USR_CMP_DTL_Car_Markup,
                        PackageMarkupAP = user.BS_APPSECMOD_EXT_USR_CMP_DTL_PackageMarkup_A_P,
                        PackageMarkupAmt = user.BS_APPSECMOD_EXT_USR_CMP_DTL_Package_Markup,
                        AllowOptionBooking = user.BS_APPSECMOD_EXT_USR_CMP_DTL_Allow_Option_Booking,
                        Otp = Convert.ToInt32(user?.BS_APPSECMOD_USR_INF_Btres_OTP ?? 1),
                        OtpDate = user.BS_APPSECMOD_USR_INF_OTP_Date,
                        OtpEmailId = user.BS_APPSECMOD_USR_INF_OTP_Email,
                        UserID = user.BS_APPSECMOD_USR_INF_UserId,
                        BusinessCurrency = user.BS_APPSECMOD_EXT_USR_CMP_DTL_Currency_Supplier,
                        CreditLimit = Convert.ToDecimal(user.BS_APPSECMOD_EXT_USR_CMP_DTL_Credit_Limit)

                    }
                    );
                    userList = result?.ToList() ?? new();
                }
                catch (Exception ex)
                {

                }
            }
            return userList;
        }
        //public List<AccountDTO> GetAccountInfo(AccountDTO objAccount)
        //{
        //    //objAccount = NullifyClass.GetNullfiyAccountInfo(objAccount);
        //    List<AccountDTO> accountResultList = new List<AccountDTO>();
        //    using (var shellEntity = (SHELLEntity?)_dataContext.GetDBContext(MyEnum.Database.SHELL))
        //    {
        //        try
        //        {
        //            var parameters = new DynamicParameters();
        //            parameters.Add("@paramFName", objAccount?.UserInfo?.FirstName);
        //            parameters.Add("@paramEmailId", objAccount?.UserInfo?.Email);
        //            parameters.Add("@paramPostalCode", objAccount?.Street?.PostalCode);
        //            parameters.Add("@paramMbNo", objAccount?.UserInfo?.ContactNo);
        //            parameters.Add("@paramLandNo", objAccount?.UserInfo?.LandlineNo);
        //            parameters.Add("@paramAccCode", objAccount?.AccountCode);
        //            parameters.Add("@paramCompCode", objAccount?.CompanyCode);
        //            parameters.Add("@paramCompName", objAccount?.CompanyName);
        //            parameters.Add("@paramIsActive", objAccount?.IsActive);
        //            connection.Open();
        //            var results = connection.Query<GETACCOUNTINFO>("sp_BS_APPSECMOD_User_Information_SearchUserID_Get", parameters, commandType: CommandType.StoredProcedure)
        //                            .Select(result => new AccountDTO
        //                            {
        //                                AccountCode = result?.AccountCode,
        //                                CompanyCode = result?.CompanyCode,
        //                                CompanyName = result?.CompanyName,
        //                                CompanyEmail = result?.CompanyEmail,
        //                                AccountType = result?.AccessType?.ToUpper(),
        //                                IsActive = result?.ActiveBlockUser,
        //                                TeamManager = result?.T2Managername,
        //                                Remark = result?.Remark,
        //                                ImpInfo = result?.BTIMSmsg,
        //                                ATOL = result?.ATOLNO,
        //                                SAFI = result?.SAFI,
        //                                TTA = result?.TTA,
        //                                WorldChoice = result?.WORLDCHOICE,
        //                                Global = result?.GLOBALVal,
        //                                Advantage = result?.ADVANTAGE,
        //                                ABTA = result?.ABTA,
        //                                CreditLimit = decimal.Parse(result?.CreditLimit.ToString()),
        //                                RedCreditLimit = decimal.Parse(result?.RedCredit_Limit.ToString()),
        //                                CurrencySupplier = result?.CurrencySupplier,
        //                                CreditTerms = result?.CreditTerms,
        //                                CreditRiskRating = result?.Credit_Risk_Rating,
        //                                DirectorRemark = result?.Director_Remark,
        //                                Markup = Convert.ToDecimal(result?.Markup),
        //                                MarkupType = result?.MarkupType,
        //                                GDSInfo = result?.GDSInfo,
        //                                WhiteLabelInfo = result?.WLInfo,
        //                                FareInfo = result?.FareInfo,
        //                                SubCompanyName = result?.SubComp_Name,
        //                                UserInfo = new UserDTO
        //                                {
        //                                    UserID = result?.UserID,
        //                                    Title = result?.Gender,
        //                                    FirstName = result?.FirstName,
        //                                    LastName = result?.LastName,
        //                                    ContactNo = result?.MobileNo,
        //                                    LandlineNo = result?.PhoneNo,
        //                                    Email = result?.EmailID,
        //                                    Fax = result?.Fax
        //                                },
        //                                LoginInfo = new LoginDTO
        //                                {

        //                                    AccessType = result?.AccessType,

        //                                },
        //                                Street = new StreetDTO { HouseNo = result?.HouseNumber, PostalCode = result?.PostalCode, Address1 = result?.Street },
        //                                City = new CityDTO { CityName = result?.City },
        //                                State = result?.State,
        //                                Country = new CountryDTO { CountryName = result?.Country },
        //                                ContactinBrightsun = result?.ContactinBrightsun,
        //                                MarkupInfo = new MarkupDTO
        //                                {

        //                                    FlightMarkupAmount = Convert.ToDecimal(result?.FlightMarkup_Amt),
        //                                    HotelMarkupAmount = Convert.ToDecimal(result?.HotelMarkup_Amt),
        //                                    HotelMarkupType = Convert.ToChar(result?.HotelMarkup_Type),
        //                                    FlightMarkupType = Convert.ToChar(result?.FlightMarkup_Type),
        //                                    CarMarkupAmount = Convert.ToDecimal(result?.CarMarkup_Amt),
        //                                    CarMarkupType = Convert.ToChar(result?.CarMarkup_Type),
        //                                    InsuranceMarkupAmount = Convert.ToDecimal(result?.InsMarkup_Amt),
        //                                    InsuranceMarkupType = Convert.ToChar(result?.InsMarkup_Type)
        //                                }
        //                            });
        //            dbHelper.DisposeConnection(connection);
        //            try
        //            {
        //                accountResultList = results.ToList();
        //                return accountResultList;
        //            }
        //            catch
        //            {
        //                return new List<AccountDTO>();
        //            }
        //        }
        //    }
        //    return new List<AccountDTO>();
        //}
        public void Dispose()
        {
            if (_disposed) return;
        }
    }
}
