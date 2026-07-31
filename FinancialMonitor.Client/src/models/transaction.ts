import type { Currency } from "../enums/currency";
import type { TransactionStatus } from "../enums/transactionStatus";

export interface Transaction {
    transactionId: string;
    amount: number;
    currency: Currency;
    status: TransactionStatus;
    timestamp: string;
}

export interface CreateTransactionRequest {
    amount: number;
    currency: Currency;
}