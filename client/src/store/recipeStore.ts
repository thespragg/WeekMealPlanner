import { defineStore } from "pinia";
import { recipes } from "@/api/recipes"
import { IRecipe } from "@/types/IRecipe";
import { IRecipeList } from "@/types/IRecipeList";

type State = {
    recipes: IRecipe[],
    recipeNames: IRecipeList
}

export const useIngredientStore = defineStore("ingredientStore", {
    state: (): State => ({
        recipes: [],
        recipeNames: {}
    }),

    actions: {
        getRecipes: async (state: State) => {
            state.recipes = await recipes.get();
            state.recipeNames = await recipes.names();
        },
    },
});