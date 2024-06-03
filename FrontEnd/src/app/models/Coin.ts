export interface Coin {
  id: string;
  symbol: string;
  name: string;
  currentPrice: number;
  marketCap: number;
  marketCapRank: number;
  high24h: number;
  low24h: number;
  priceChange24h: number;
  priceChangePercentage24h: number;
  marketCapChange24h: number;
  marketCapChangePercentage24h: number;
  ath: number;
  imageUrl: string;
}
