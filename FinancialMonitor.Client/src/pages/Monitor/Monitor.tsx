import { useEffect, useRef, useState } from "react";
import type { Transaction } from "../../models/transaction";
import { TransactionStatus } from "../../enums/transactionStatus";
import { startSignalR, stopSignalR } from "../../services/signalRService";
import TransactionTable from "../../components/TransactionTable/TransactionTable";
import "./Monitor.css";

export default function Monitor() {
    const [transactions, setTransactions] = useState<Transaction[]>([]);
    const [newTransactionId, setNewTransactionId] = useState<string | null>(null);
    const [showErrors, setShowErrors] = useState(false);

    const timer = useRef<ReturnType<typeof setTimeout> | null>(null);

    useEffect(() => {
        startSignalR(transaction => {
            setTransactions(previous => {
                const index = previous.findIndex(
                    x => x.transactionId === transaction.transactionId
                );

                if (index === -1) {
                    setNewTransactionId(transaction.transactionId);

                    if (timer.current) {
                        clearTimeout(timer.current);
                    }

                    timer.current = setTimeout(() => {
                        setNewTransactionId(null);
                    }, 1200);

                    return [
                        transaction,
                        ...previous
                    ];
                }

                const updated = [...previous];

                updated[index] = transaction;

                setNewTransactionId(transaction.transactionId);

                if (timer.current) {
                    clearTimeout(timer.current);
                }

                timer.current = setTimeout(() => {
                    setNewTransactionId(null);
                }, 1200);

                return updated;
            });
        });

        return () => {
            stopSignalR();

            if (timer.current) {
                clearTimeout(timer.current);
            }
        };
    }, []);

    const displayedTransactions = showErrors
        ? transactions.filter(
            transaction =>
                transaction.status === TransactionStatus.Failed
          )
        : transactions;

    return (
        <div className="monitor-page">
            <div className="monitor-header">
                <h1>
                    Live Financial Monitor
                </h1>

                <label className="filter">
                    <input
                        type="checkbox"
                        checked={showErrors}
                        onChange={e =>
                            setShowErrors(e.target.checked)
                        }
                    />
                    Show only Errors
                </label>
            </div>

            <TransactionTable
                transactions={displayedTransactions}
                newTransactionId={newTransactionId}
            />
        </div>
    );
}