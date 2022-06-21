<template>
    <NCard size="small" title="Shopping List">
        <!-- <p v-for="item in shoppingList" :key="item.item.name">{{ item.quantity }}x {{ item.item.name }}</p> -->
        <p v-for="recipe in recipes" :key="recipe">{{recipe}}</p>
    </NCard>
</template>

<script setup lang="ts">
import { useMealStore } from '@/store/mealStore';
import { IDayPlan } from '@/types/IDayPlan';
import { IShoppingListItem } from '@/types/IShoppingListItem';
import { IWeekPlan } from '@/types/IWeekPlan';
import { NCard } from "naive-ui"
import { computed, onBeforeMount, ref } from 'vue';

const mealStore = useMealStore();
const recipes = computed<{
    [name: string]: number
}>(() => {
    const dayKeys = ["saturday", "sunday", "monday", "tuesday", "wednesday", "thursday", "friday"] as (keyof IWeekPlan)[];
    const mealKeys = ["lunch", "dinner"] as (keyof IDayPlan)[];

    const res: {
        [name: string]: number
    } = {}
    for (const day of dayKeys) {
        for (const meal of mealKeys) {
            const recipe = mealStore.plan[day][meal];
            if (!recipe) continue;
            if (res[recipe]) res[recipe]++;
            else res[recipe] = 1;
        }
    }
    return res;
})

const shoppingList = ref<IShoppingListItem[]>([]);
const calculateShoppingList = () => {
    const allIngredients = [] as any[]; //props.recipes.map(x => x.ingredients).flat();
    const res: IShoppingListItem[] = [];

    for (const ingredient of allIngredients) {
        const index = res.findIndex(x => x.item.name == ingredient.item?.name);
        if (index === -1) {
            res.push({
                quantity: Math.ceil(ingredient.quantity! / ingredient.item?.quantity!),
                used: ingredient.quantity!,
                item: ingredient.item!
            })
        } else {
            res[index].used += ingredient.quantity!;
            res[index].quantity = Math.ceil(res[index].used / ingredient.item?.quantity!);
        }
    }

    shoppingList.value = res;
}

onBeforeMount(() => calculateShoppingList());
</script>