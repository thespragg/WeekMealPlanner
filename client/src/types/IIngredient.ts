import { IShopItem } from "./IShopItem";
import { Unit } from "./Unit";

export interface IIngredient {
    id: number;
    item: IShopItem | null;
    quantity: number | null;
    unit: Unit | null;
}