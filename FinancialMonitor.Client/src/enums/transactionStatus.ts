export const TransactionStatus = {
    Pending: 0,
    Completed: 1,
    Failed: 2
} as const;


export type TransactionStatus =
    typeof TransactionStatus[keyof typeof TransactionStatus];