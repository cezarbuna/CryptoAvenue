export interface TransactionDto {
  walletId: string;
  sourceCoinId: string;
  sourceQuantity: number;
  sourcePrice: number;
  targetCoinId: string;
  targetQuantity: number;
  targetPrice: number;
  transactionDate: string; // DateOnly
  transactionTime: string; // TimeOnly
  transactionType: string;
}
