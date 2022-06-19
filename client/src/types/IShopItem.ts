import { Unit } from "./Unit";

export interface IShopItem {
    id: number;
    name: string;
    quantity: number;
    price: number;
    unit: Unit;
}