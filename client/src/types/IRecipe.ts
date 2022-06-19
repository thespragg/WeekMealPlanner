import { IIngredient } from "./IIngredient";

export interface IRecipe {
    id: number;
    name: string;
    ingredients: IIngredient[];
}