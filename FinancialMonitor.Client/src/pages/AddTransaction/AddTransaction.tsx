import { useState } from "react";
import { Currency } from "../../enums/currency";
import { createTransaction } from "../../api/transactionApi";
import "./AddTransaction.css";

export default function AddTransaction() {
    const [amount, setAmount] = useState<number>(0);
    const [currency, setCurrency] = useState<Currency>(Currency.USD);
    const [loading, setLoading] = useState(false);
    const [message, setMessage] = useState("");

    async function handleCreate() {
        setLoading(true);
        setMessage("");

        try {
            await createTransaction(
                amount,
                currency
            );
            setMessage(
                "Transaction created successfully"
            );
            setAmount(0);
        }
        catch {
            setMessage(
                "Failed creating transaction"
            );
        }
        finally {
            setLoading(false);
        }
    }

    function generateRandom() {
        const currencies: Currency[] = [
            Currency.USD,
            Currency.EUR,
            Currency.ILS
        ];

        setAmount(
            Number(
                (
                    Math.random() * 2000 - 1000
                ).toFixed(2)
            )
        );

        setCurrency(
            currencies[
                Math.floor(
                    Math.random() * currencies.length
                )
            ]
        );
    }

    return (
        <div className="add-page">
            <div className="transaction-card">
                <h1>
                    Create Transaction
                </h1>
                <div className="form-group">
                    <label>
                        Amount
                    </label>
                    <input
                        type="number"
                        value={amount}
                        onChange={e =>
                            setAmount(
                                Number(e.target.value)
                            )
                        }
                    />
                </div>
                <div className="form-group">
                    <label>
                        Currency
                    </label>
                    <select
                        value={currency}
                        onChange={e =>
                            setCurrency(
                                Number(e.target.value) as Currency
                            )
                        }
                    >
                        <option value={Currency.USD}>
                            USD $
                        </option>
                        <option value={Currency.EUR}>
                            EUR €
                        </option>
                        <option value={Currency.ILS}>
                            ILS ₪
                        </option>
                    </select>
                </div>
                <div className="actions">
                    <button
                        className="generate"
                        onClick={generateRandom}
                    >
                        Generate Random
                    </button>
                    <button
                        className="create"
                        disabled={loading}
                        onClick={handleCreate}
                    >
                        {
                            loading
                                ? "Creating..."
                                : "Create Transaction"
                        }
                    </button>
                </div>
                {
                    message &&
                    <div className="message">
                        {message}
                    </div>
                }
            </div>
        </div>
    );
}