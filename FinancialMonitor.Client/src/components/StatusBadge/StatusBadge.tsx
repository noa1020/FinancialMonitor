import { TransactionStatus } from "../../enums/transactionStatus";
import "./StatusBadge.css";

interface Props {
    status: number;
}

export default function StatusBadge({ status }: Props) {
    const getStatus = () => {
        switch (status) {
            case TransactionStatus.Completed:
                return {
                    text: "Completed",
                    className: "completed"
                };

            case TransactionStatus.Failed:
                return {
                    text: "Failed",
                    className: "failed"
                };

            default:
                return {
                    text: "Pending",
                    className: "pending"
                };
        }
    };

    const result = getStatus();

    return (
        <span className={`status-badge ${result.className}`}>
            {result.text}
        </span>
    );
}