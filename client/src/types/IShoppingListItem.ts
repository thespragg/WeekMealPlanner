import { IShopItem } from "./IShopItem";

export interface IShoppingListItem {
    quantity: number,
    used: number,
    item: IShopItem
}