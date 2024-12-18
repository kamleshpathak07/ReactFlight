using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReactFlight.Server.InfraLayer.DATA
{
    public class BrightsunEntity : DbContext
    {
        public BrightsunEntity(DbContextOptions<BrightsunEntity> options) : base(options) { }
        public DbSet<Airport_AutoComplete> airport_AutoCompletes { get; set; }
        public DbSet<UserInformation_CompCode_UserID> userInformation_CompCode_UserIDs { get; set; }
        public DbSet<BS_Airline_Details> BS_Airline_Details { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Airport_AutoComplete>(z => z.HasNoKey());
            modelBuilder.Entity<UserInformation_CompCode_UserID>(z => z.HasNoKey());
            modelBuilder.Entity<BS_Airline_Details>(z => z.HasNoKey());
        }
    }
    public class Airport_AutoComplete
    {
        [Column("BS_CTY_City_Name")]
        public string? CityName { get; set; }
        [Column("BS_CNTRY_Country_Name")]
        public string? CountryName { get; set; }
        [Column("BS_ARPT_Airport_Code")]
        public string? AirportCode { get; set; }
        [Column("BS_ARPT_Airport_Name")]
        public string? AirportName { get; set; }
        [Column("BS_CNTRY_Country_Code")]
        public string? CountryCode { get; set; }
    }
    public class UserInformation_CompCode_UserID
    {
        public string? BS_APPSECMOD_EXT_USR_CMP_DTL_Account_code { get; set; }
        public string? BS_APPSECMOD_EXT_USR_CMP_DTL_Company_code { get; set; }
        public string? BS_APPSECMOD_EXT_USR_CMP_DTL_Company_Name { get; set; }
        public string? BS_APPSECMOD_USR_INF_City { get; set; }
        public string? BS_APPSECMOD_USR_INF_Country { get; set; }
        public string? BS_APPSECMOD_USR_INF_House_Number { get; set; }
        public string? BS_APPSECMOD_USR_INF_Phone { get; set; }
        public string? BS_APPSECMOD_USR_INF_Mobile { get; set; }
        public string? BS_APPSECMOD_USR_INF_Post_Code { get; set; }
        public string? BS_APPSECMOD_USR_INF_Street { get; set; }
        public string? BS_APPSECMOD_USR_INF_EMail_Address { get; set; }
        public string? BS_APPSECMOD_USR_INF_AccessType { get; set; }
        public string? BS_APPSECMOD_USR_INF_FirstName { get; set; }
        public string? BS_APPSECMOD_USR_INF_LastName { get; set; }
        public string? BS_APPSECMOD_EXT_USR_CMP_DTL_FlightMarkup_A_P { get; set; }
        public double BS_APPSECMOD_EXT_USR_CMP_DTL_Flight_Markup { get; set; }
        public string? BS_APPSECMOD_EXT_USR_CMP_DTL_HotelMarkup_A_P { get; set; }
        public double BS_APPSECMOD_EXT_USR_CMP_DTL_Hotel_Markup { get; set; }
        public string? BS_APPSECMOD_EXT_USR_CMP_DTL_InsuranceMarkup_A_P { get; set; }
        public double BS_APPSECMOD_EXT_USR_CMP_DTL_Insurance_Markup { get; set; }
        public string? BS_APPSECMOD_EXT_USR_CMP_DTL_CarMarkup_A_P { get; set; }
        public double BS_APPSECMOD_EXT_USR_CMP_DTL_Car_Markup { get; set; }
        public string? BS_APPSECMOD_EXT_USR_CMP_DTL_PackageMarkup_A_P { get; set; }
        public double? BS_APPSECMOD_EXT_USR_CMP_DTL_Package_Markup { get; set; }
        public bool BS_APPSECMOD_EXT_USR_CMP_DTL_Allow_Option_Booking { get; set; } = false;
        public int? BS_APPSECMOD_USR_INF_Btres_OTP { get; set; }
        public DateTime? BS_APPSECMOD_USR_INF_OTP_Date { get; set; }
        public string? BS_APPSECMOD_USR_INF_OTP_Email { get; set; }
        //[Column("BS_APPSECMOD_USR_INF_d")]
        //public string? USR_INF_d { get; set; }
        public string? BS_APPSECMOD_EXT_USR_CMP_DTL_Currency_Supplier { get; set; }
        public double BS_APPSECMOD_EXT_USR_CMP_DTL_Credit_Limit { get; set; }
        public string? BS_APPSECMOD_USR_INF_UserId { get; set; }
        //[Required]
        //[Column("BS_APPSECMOD_EXT_USR_CMP_DTL_Email")]
        //public string Company_Email { get; set; } = string.Empty;
        //[Required]
        //[Column("BS_APPSECMOD_EXT_USR_CMP_DTL_Phone_Number")]
        //public string Phone_Number { get; set; } = string.Empty;
    }
    public class BS_Airline_Details
    {
        [Column("BS_FRS_AIRLINE_AirlineCode")]
        public string? AirlineCode { get; set; }
        [Column("BS_FRS_AIRLINE_Airline")]
        public string? AirlineName { get; set; }
        [Column("BS_FRS_AIRLINE_Airline_Image_URL")]
        public string? AirlineLogo { get; set; }
    }
}
