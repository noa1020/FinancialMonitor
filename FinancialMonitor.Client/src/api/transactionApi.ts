import { Currency } from "../enums/currency";

const API_URL =`${import.meta.env.VITE_API_URL}/api/transactions`;

export async function createTransaction(
    amount:number,
    currency:Currency
){

    const response=await fetch(API_URL,{

        method:"POST",

        headers:{
            "Content-Type":"application/json"
        },

        body:JSON.stringify({
            amount,
            currency
        })

    });

    if(!response.ok){
        throw new Error("Failed creating transaction.");
    }

}