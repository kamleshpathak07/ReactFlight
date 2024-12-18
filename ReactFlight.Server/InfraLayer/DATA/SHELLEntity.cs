using Microsoft.EntityFrameworkCore;

namespace ReactFlight.Server.InfraLayer.DATA
{
    public class SHELLEntity : DbContext
    {
        public SHELLEntity(DbContextOptions<SHELLEntity> options) : base(options)
        {
        }
        public DbSet<GETACCOUNTINFO> GetACCOUNTINFO { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<GETACCOUNTINFO>().HasNoKey();
        }
    }
    public partial class GETACCOUNTINFO
    {
        public string? AccountCode { get; set; }
        public string? CompanyCode { get; set; }
        public string? CompanyName { get; set; }
        public string? CompanyEmail { get; set; }
        public string? AccessType { get; set; }
        public string? ActiveBlockUser { get; set; }
        public string? T2Managername { get; set; }
        public string? Remark { get; set; }
        public string? BTIMSmsg { get; set; }
        public string? ATOLNO { get; set; }
        public string? SAFI { get; set; }
        public string? TTA { get; set; }
        public string? WORLDCHOICE { get; set; }
        public string? GLOBALVal { get; set; }
        public string? ADVANTAGE { get; set; }
        public string? ABTA { get; set; }
        public double? CreditLimit { get; set; }
        public double? RedCredit_Limit { get; set; }
        public string? CurrencySupplier { get; set; }
        public string? CreditTerms { get; set; }
        public string? Credit_Risk_Rating { get; set; }
        public string? Director_Remark { get; set; }
        public decimal Markup { get; set; }
        public string? MarkupType { get; set; }
        public string? GDSInfo { get; set; }
        public string? WLInfo { get; set; }
        public string? FareInfo { get; set; }
        public string? SubComp_Name { get; set; }
        public string? UserID { get; set; }
        public string? Gender { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? MobileNo { get; set; }
        public string? PhoneNo { get; set; }
        public string? EmailID { get; set; }
        public string? Fax { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public string? ContactinBrightsun { get; set; }
        public decimal FlightMarkup_Amt { get; set; }
        public decimal HotelMarkup_Amt { get; set; }
        public char HotelMarkup_Type { get; set; }
        public char FlightMarkup_Type { get; set; }
        public decimal CarMarkup_Amt { get; set; }
        public char CarMarkup_Type { get; set; }
        public decimal InsMarkup_Amt { get; set; }
        public char InsMarkup_Type { get; set; }
        public string? HouseNumber { get; set; }
        public string? PostalCode { get; set; }
    }
}
