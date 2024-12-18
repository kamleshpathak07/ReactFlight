using Microsoft.EntityFrameworkCore;

namespace ReactFlight.Server.InfraLayer.DATA
{
    public class IBEEntity : DbContext
    {
        public IBEEntity(DbContextOptions<IBEEntity> options) : base(options)
        {
        }
        public DbSet<Booking_Ref_IDs> Booking_Ref_IDs { get; set; }
        public DbSet<ProductSearchBookingDetails> ProductSearchBookingDetails { get; set; }
        public DbSet<TransactionStatus> TransactionStatus { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Booking_Ref_IDs>(z => z.HasNoKey());
            modelBuilder.Entity<ProductSearchBookingDetails>(z => z.HasNoKey());
            modelBuilder.Entity<TransactionStatus>(z => z.HasNoKey());
        }
    }
    public partial class Booking_Ref_IDs
    {
        public string? BookingReference { get; set; }
    }
    [Serializable]
    public partial class ProductSearchBookingDetails
    {
        public string? Prod_Booking_ID { get; set; }
        public string? ProdRef { get; set; }
        public string? Supplier_Ref { get; set; }
        public string? Booking_Ref { get; set; }
        public string? Booking_Status { get; set; }
        public DateTime Booking_Date_Time { get; set; }
        public string? Booking_Media { get; set; }
        public DateTime? Status_Date { get; set; }
        public string? Prod_Booking_Status { get; set; }
        public string? Meta_Search { get; set; }
        public string? Booking_By { get; set; }
        public string? Booking_By_Type { get; set; }
        public string? Company_Name { get; set; }
        public string? Company_Email { get; set; }
        public string? Company { get; set; }
        public decimal? Total_Amount { get; set; }
        public string? Currency { get; set; }
        public string? AgentRef { get; set; }
        public string? Advert_Media_Code { get; set; }
        public string? PNR_Confirmation { get; set; }
        public string? Universal_Record_Locator { get; set; }
        //  public DateTime CheckIn_Date { get; set; }
        public string? Destination { get; set; }
        //public DateTime CheckOut_Date { get; set; }
        //public DateTime HotelOptionDate { get; set; }
        //public string? Hotel_Name { get; set; }
        //public string? Hotel_Code { get; set; }
        //public string? Hotel_City { get; set; }
        //public string? Contact_No { get; set; }
        //public string? EmergencyContact { get; set; }
        //public string? HotelCancellationPolicy { get; set; }
        //public string? HotelAddress { get; set; }
        public string? Operator_ID { get; set; }
        public string? First_Name { get; set; }
        public string? Last_Name { get; set; }
        // public string? Middle_Name { get;  set; }
        public bool? IsLeadPax { get; set; }
        public string? PaxType { get; set; }
        public string? Pax_ID { get; set; }
        //public string? MealCode { get;  set; }
        //public string? MealDesc { get;  set; }
        //public string? Room_Name { get;  set; }
        //public string? Room_Code { get;  set; }
        //public string? RoomRemarks { get;  set; }
        //public string? IBE_ROM_DTL_Room_Ref { get;  set; }
        //public string? RoomPaxID { get;  set; }
        //public int? NoOfPax { get;  set; }
        //public string? IBE_ROM_DTL_Room_Confirmation_No { get;  set; }
        //public decimal? IBE_ROM_DTL_Room__Cost_Price { get;  set; }
        //public decimal? IBE_ROM_DTL_Room__Sell_Price { get;  set; }
        public bool? IsClearedQC { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string? PaymentRemark { get; set; }
        public bool? IsBreakDownShow { get; set; }
        public string? Seg_Id { get; set; }
        public string? Origin { get; set; }
        public string? FromDestination { get; set; }
        public string? ToDestination { get; set; }
        public DateTime? Depart_Date { get; set; } = null;
        public DateTime? Arrival_Date { get; set; } = null;
        public string? CarierCode { get; set; }
        public string? FlightNo { get; set; }
        public string? Class { get; set; }
        public string? SegmentStatus { get; set; }
        public string? SegRemarks { get; set; }
        public bool? IsSegRemarkVisibleToCust { get; set; }
        public bool? ChangeOfPlane { get; set; }
        public int? NumberOfStops { get; set; }
        public string? OperatingCarrier { get; set; }
        public string? OperatingFlightNo { get; set; }
        //public string? Booking_Type_Id { get;  set; }
        //public string? Depot_Description { get;  set; }
        //public bool Allowed_Luggage { get;  set; }
        //public bool Product_Id { get;  set; }
        //public string? Transfer_Name { get;  set; }
        //public bool Product_Type_Id { get;  set; }
        //public bool TravelTime { get;  set; }
        //public bool ReturnProductId { get;  set; }
        //public string? Supplier_Contact { get;  set; }
        //public string? PickUp_Location { get;  set; }
        //public bool From_Date { get;  set; }
        //public string? DropOff_Location { get;  set; }
        //public bool To_Date { get;  set; }
        //public bool Allowed_Adult { get;  set; }
        //public bool Allowed_Child { get;  set; }
        //public bool Allowed_Infant { get;  set; }
        //public string? Hotel_Address { get;  set; }
        //public string? Hotel_Country { get;  set; }
        ////public string? Airport_Name { get;  set; }
        //public string? Terminal { get;  set; }
        //public string? Transfer_Contact_Name { get;  set; }
        //public string? Brightsun_Emergency_Number { get;  set; }
        //public string? Transfer_Contact_Number { get;  set; }
        //public string? Meeting_Point { get;  set; }
        public string? Baggageallownce { get; set; }
        public string? Farebasis { get; set; } = string.Empty;
        //public string? Farebasisi { get; set; }
        public string? Arpttrminal { get; set; }
        public string? Gender { get; set; }
        public string? Passport_No { get; set; } = string.Empty;
        public DateTime DOB { get; set; }
        public DateTime? PCR_TestDateTime { get; set; }
        //public string? LstPassengerDetail { get;  set; } = string.Empty;
        //public string? LstTravelDetail { get;  set; } = string.Empty;
        public string? Approval_Status { get; set; }
    }
    public partial class TransactionStatus
    {
        public string? INSERT_STATUS { get; set; }
        public string? DESCRIPITON { get; set; }

    }
}
