import { IShopItem } from "@/types/IShopItem";
import { defineStore } from "pinia";
import { shop } from "@/api/shop"

type State = {
    shop: IShopItem[]
}

export const useShopItemStore = defineStore("shopItemStore", {
    state: (): State => ({
        shop: []
    }),

    actions: {
        getItems: async (state: State) => state.shop = await shop.get(),
        byId: (state: State, id: number): IShopItem | null => {
            const index = state.shop.findIndex(x => x.id == id);
            if (index === -1) return null;
            return state.shop[index];
        }
    }
});