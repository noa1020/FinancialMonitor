import type { Transaction } from "../../models/transaction";
import { Currency } from "../../enums/currency";
import StatusBadge from "../StatusBadge/StatusBadge";
import "./TransactionTable.css";

interface Props{
    transactions:Transaction[];
    newTransactionId:string|null;
}

const symbols={
    [Currency.USD]:"$",
    [Currency.EUR]:"€",
    [Currency.ILS]:"₪"
};

export default function TransactionTable({
    transactions,
    newTransactionId
}:Props){

    return(

        <div className="table-card">

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
                        transactions.map(transaction=>(

                            <tr
                                key={transaction.transactionId}
                                className={
                                    transaction.transactionId===newTransactionId
                                    ?"new-row"
                                    :""
                                }
                            >

                                <td>

                                    {
                                        symbols[
                                            transaction.currency
                                        ]
                                    }

                                    {
                                        transaction.amount.toFixed(2)
                                    }

                                </td>

                                <td>

                                    <StatusBadge
                                        status={transaction.status}
                                    />

                                </td>

                                <td>

                                    {
                                        new Date(
                                            transaction.timestamp
                                        ).toLocaleTimeString()
                                    }

                                </td>

                            </tr>

                        ))
                    }

                </tbody>

            </table>

        </div>

    );

}