export const Currency = {
    USD: 0,
    EUR: 1,
    ILS: 2
} as const;

export type Currency =
    typeof Currency[keyof typeof Currency];