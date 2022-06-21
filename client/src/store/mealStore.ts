import { IDayPlan } from "@/types/IDayPlan";
import { IWeekPlan } from "@/types/IWeekPlan";
import { defineStore } from "pinia";

type State = {
    plan: IWeekPlan
}

export const useMealStore = defineStore("mealStore", {
    state: (): State => ({
        plan: {
            monday: {
                lunch: undefined,
                dinner: undefined
            },
            tuesday: {
                lunch: undefined,
                dinner: undefined
            },
            wednesday: {
                lunch: undefined,
                dinner: undefined
            },
            thursday: {
                lunch: undefined,
                dinner: undefined
            },
            friday: {
                lunch: undefined,
                dinner: undefined
            },
            saturday: {
                lunch: undefined,
                dinner: undefined
            },
            sunday: {
                lunch: undefined,
                dinner: undefined
            }
        }
    }),

    actions: {
        setRecipe: (state: State, day: keyof IWeekPlan, meal: keyof IDayPlan, recipeId: number) => state.plan[day][meal] = recipeId
    }
});