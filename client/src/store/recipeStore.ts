import { defineStore } from "pinia";
import { recipes } from "@/api/recipes"
import { IRecipe } from "@/types/IRecipe";
import { IRecipeList } from "@/types/IRecipeList";

type State = {
    recipes: IRecipe[],
    recipeNames: IRecipeList
}

export const useRecipeStore = defineStore("recipeStore", {
    state: (): State => ({
        recipes: [],
        recipeNames: {}
    }),

    actions: {
        async getRecipes() {
            this.recipes = await recipes.get();
            this.recipeNames = await recipes.names();
        },
        async create(newRecipe: IRecipe){
            const savedRecipe = await recipes.create(newRecipe);
            this.recipes.push(newRecipe);
            this.recipeNames[savedRecipe.id] = savedRecipe.name;
        },
        byName(name:string){
            return this.recipes.find(x=>x.name == name);
        }
    },
});