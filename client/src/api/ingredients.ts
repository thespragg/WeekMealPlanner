import { IIngredient } from "@/types/IIngredient";
import { http } from "./client"

const get = async (): Promise<IIngredient[]> => await http.get("ingredients");
const getById = async (id: number): Promise<IIngredient> => await http.get(`ingredients/${id}`);
const update = async (ingredient: IIngredient): Promise<IIngredient> => await http.put("ingredients/update", ingredient);
const create = async (ingredient: IIngredient): Promise<IIngredient> => await http.post("ingredients/create", ingredient);

export const ingredients = {
    get,
    getById,
    update,
    create
}