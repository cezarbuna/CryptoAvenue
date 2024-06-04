namespace CryptoAvenue.Dtos.TransactionDtos
{
    public class TransactionGetDto
    {
        public Guid WalletId { get; set; }
        public string SourceCoinId { get; set; }
        public double SourceQuantity { get; set; }
        public double SourcePrice { get; set; }
        public string TargetCoinId { get; set; }
        public double TargetQuantity { get; set; }
        public double TargetPrice { get; set; }
        public DateOnly TransactionDate { get; set; }
        public TimeOnly TransactionTime { get; set; }
        public string TransactionType { get; set; }
    }
}
