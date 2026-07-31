import { memo } from "react";
import { Currency } from "../../enums/currency";
import type { Transaction } from "../../models/transaction";
import StatusBadge from "../StatusBadge/StatusBadge";

interface Props {
    transaction: Transaction;
    isNew: boolean;
}

const currencyConfig = {
    [Currency.USD]: "$",
    [Currency.EUR]: "€",
    [Currency.ILS]: "₪"
};

function TransactionRow({
    transaction,
    isNew
}: Props) {
    function formatAmount(
        amount: number,
        currency: number
    ) {
        const symbol =
            currencyConfig[currency as Currency] ?? "";

        return `${symbol}${amount.toFixed(2)}`;
    }

    return (
        <tr
            className={
                isNew
                    ? "new-row"
                    : ""
            }
        >
            <td>
                {
                    formatAmount(
                        transaction.amount,
                        transaction.currency
                    )
                }
            </td>

            <td>
                <StatusBadge
                    status={transaction.status}
                />
            </td>

            <td>
                {
                    new Date(transaction.timestamp)
                        .toLocaleTimeString()
                }
            </td>
        </tr>
    );
}

export default memo(TransactionRow);