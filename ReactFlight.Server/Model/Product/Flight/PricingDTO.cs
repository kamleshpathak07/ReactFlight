namespace ReactFlight.Server.Model.Product.Flight
{
    public class PricingDTO
    {
        public string? PaxType { get; set; } = string.Empty;
        public decimal? ProductPrice { get; set; } = 0;
        public decimal? TotalPrice { get; set; } = 0;
        public decimal? BasePrice { get; set; } = 0;
        public decimal? DuePrice { get; set; } = 0;
        public decimal? PaidPrice { get; set; } = 0;
        public decimal? PayingPrice { get; set; } = 0;
        public decimal? Tax { get; set; } = 0;
        public decimal? SupplierCostPrice { get; set; } = 0;
        public decimal? SupplierSellPrice { get; set; } = 0;
    }
}
