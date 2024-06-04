export interface TransactionDto {
  transactionId: string;
  walletId: string;
  sourceCoinId: string;
  sourceCoinImageUrl: string;
  sourceQuantity: number;
  sourcePrice: number;
  targetCoinId: string;
  targetCoinImageUrl: string;
  targetQuantity: number;
  targetPrice: number;
  transactionDate: string; // DateOnly
  transactionTime: string; // TimeOnly
  transactionType: string;
}
