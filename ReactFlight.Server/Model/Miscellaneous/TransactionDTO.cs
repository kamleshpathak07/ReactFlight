
namespace ReactFlight.Server.Model.Miscellaneous
{
    [Serializable]
    public class TransactionDTO
    {
        public string? TransactionNO { get; set; } = string.Empty;
        public string? TransactionRTNO { get; set; } = string.Empty;
        public string? TransactionMedia { get; set; } = string.Empty;
        public string? TransactionType { get; set; } = string.Empty;
        public string? PaymentStatus { get; set; } = string.Empty;
        public string? PaymentOption { get; set; } = string.Empty;
        public string? PaymentBy { get; set; } = string.Empty;
        public decimal? TransactionAmount { get; set; } = 0;
        public string RecievedBy { get; set; } = string.Empty;
        public string? Remarks { get; set; } = string.Empty;
        public string CurrencyType { get; set; } = string.Empty;
        public DateTime BankingDateTime { get; internal set; }
        public decimal CardCharges { get; internal set; }
        public decimal InsuranceCharges { get; internal set; }
        public decimal AncilliaryCharges { get; internal set; }
        public decimal Discount { get; internal set; }
        public string? CRDChargesType { get; internal set; }
     
    }
}
