import type { Transaction } from "../../models/transaction";
import TransactionRow from "../TransactionRow/TransactionRow";
import "./TransactionTable.css";

interface Props {
    transactions: Transaction[];
    newTransactionId?: string | null;
}

export default function TransactionTable({
    transactions,
    newTransactionId
}: Props) {
    return (
        <div className="table-container">
            <table className="transaction-table">
                <thead>
                    <tr>
                        <th>
                            Amount
                        </th>

                        <th>
                            Status
                        </th>

                        <th>
                            Time
                        </th>
                    </tr>
                </thead>

                <tbody>
                    {
                        transactions.map(transaction => (
                            <TransactionRow
                                key={transaction.transactionId}
                                transaction={transaction}
                                isNew={
                                    transaction.transactionId === newTransactionId
                                }
                            />
                        ))
                    }
                </tbody>
            </table>
        </div>
    );
}