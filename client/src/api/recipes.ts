import { IRecipe } from "@/types/IRecipe";
import { IRecipeList } from "@/types/IRecipeList";
import { http } from "./client"

const get = async (): Promise<IRecipe[]> => await http.get("recipes");
const getById = async (id: number): Promise<IRecipe> => await http.get(`recipes/${id}`);
const names = async (): Promise<IRecipeList> => await http.get("recipes/names");
const update = async (recipe: IRecipe): Promise<IRecipe> => await http.put("recipes/update", recipe);
const create = async (recipe: IRecipe): Promise<IRecipe> => await http.post("recipes/create", recipe);

export const recipes = {
    get,
    getById,
    names,
    update,
    create
}