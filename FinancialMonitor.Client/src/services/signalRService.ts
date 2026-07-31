import {
    HubConnection,
    HubConnectionBuilder,
    HubConnectionState,
    LogLevel
} from "@microsoft/signalr";
import type { Transaction } from "../models/transaction";

const HUB_URL = `${import.meta.env.VITE_API_URL}/transactionHub`;
let connection: HubConnection | null = null;
let starting: Promise<void> | null = null;

export async function startSignalR(
    onReceive: (transaction: Transaction) => void
) {

    if (
        connection &&
        connection.state === HubConnectionState.Connected
    ) {
        return;
    }

    if (starting) {
        return starting;
    }

    connection = new HubConnectionBuilder()
        .withUrl(HUB_URL)
        .withAutomaticReconnect()
        .configureLogging(LogLevel.Information)
        .build();

    connection.on(
        "TransactionUpdated",
        onReceive
    );

    starting = connection
        .start()
        .finally(() => {
            starting = null;
        });

    await starting;
}

export async function stopSignalR() {

    if (!connection) {
        return;
    }

    if (connection.state !== HubConnectionState.Disconnected) {
        await connection.stop();
    }

    connection = null;
}