namespace CryptoAvenue.Dtos.CoinDtos
{
    public class CoinGetDto
    {
        public string Id { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public double Current_Price { get; set; }
        public double Market_Cap { get; set; }
        public int Market_Cap_Rank { get; set; }
        public double? Fully_Diluted_Valuation { get; set; }
        public double Total_Volume { get; set; }
        public double High_24h { get; set; }
        public double Low_24h { get; set; }
        public double Price_Change_24h { get; set; }
        public double Price_Change_Percentage_24h { get; set; }
        public double Market_Cap_Change_24h { get; set; }
        public double Market_Cap_Change_Percentage_24h { get; set; }
        public double Circulating_Supply { get; set; }
        public double? Total_Supply { get; set; }
        public double? Max_Supply { get; set; }
        public double Ath { get; set; }
        public double Ath_Change_Percentage { get; set; }
        public DateTime Ath_Date { get; set; }
        public double Atl { get; set; }
        public double Atl_Change_Percentage { get; set; }
        public DateTime Atl_Date { get; set; }
        // ROI might be a complex object or null if it's not provided.
        // Add a property for it if required, depending on its structure.
        public string Last_Updated { get; set; }
    }
    
}
