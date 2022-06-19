import { IShopItem } from "@/types/IShopItem";
import { http } from "./client"

const get = async (): Promise<IShopItem[]> => await http.get("shopitems");
const getById = async (id: number): Promise<IShopItem> => await http.get(`shopitems/${id}`);
const update = async (item: IShopItem): Promise<IShopItem> => await http.put("shopitems/update", item);
const create = async (item: IShopItem): Promise<IShopItem> => await http.post("shopitems/create", item);

export const shop = {
    get,
    getById,
    update,
    create
}