<template>
  <NConfigProvider :theme="theme">
    <NMessageProvider>
      <NLayout class="w-full min-h-screen p-6">
        <NGrid cols="3" x-gap="6" y-gap="6" class="text-left">
          <NGridItem span="2">
            <NCard title="Meals">
              <NTable :single-line="false" striped class="table-fixed">
                <thead>
                  <tr>
                    <th class="w-1/5">Day</th>
                    <th class="w-2/5">Lunch</th>
                    <th class="w-2/5">Dinner</th>
                  </tr>
                </thead>
                <tbody>
                  <tr>
                    <td>Saturday</td>
                    <td>
                      <MealSelector day="saturday" meal="lunch" />
                    </td>
                    <td>
                      <MealSelector day="saturday" meal="dinner" />
                    </td>
                  </tr>
                  <tr>
                    <td>Sunday</td>
                    <td>
                      <MealSelector day="sunday" meal="lunch" />
                    </td>
                    <td>
                      <MealSelector day="sunday" meal="dinner" />
                    </td>
                  </tr>
                  <tr>
                    <td>Monday</td>
                    <td>
                      <MealSelector day="monday" meal="lunch" />
                    </td>
                    <td>
                      <MealSelector day="monday" meal="dinner" />
                    </td>
                  </tr>
                  <tr>
                    <td>Tuesday</td>
                    <td>
                      <MealSelector day="tuesday" meal="lunch" />
                    </td>
                    <td>
                      <MealSelector day="tuesday" meal="dinner" />
                    </td>
                  </tr>
                  <tr>
                    <td>Wednesday</td>
                    <td>
                      <MealSelector day="wednesday" meal="lunch" />
                    </td>
                    <td>
                      <MealSelector day="wednesday" meal="dinner" />
                    </td>
                  </tr>
                  <tr>
                    <td>Thursday</td>
                    <td>
                      <MealSelector day="thursday" meal="lunch" />
                    </td>
                    <td>
                      <MealSelector day="thursday" meal="dinner" />
                    </td>
                  </tr>
                  <tr>
                    <td>Friday</td>
                    <td>
                      <MealSelector day="friday" meal="lunch" />
                    </td>
                    <td>
                      <MealSelector day="friday" meal="dinner" />
                    </td>
                  </tr>
                </tbody>
              </NTable>
            </NCard>
          </NGridItem>
          <NGridItem span="1">
            <NGrid cols="1" y-gap="6">
              <NGridItem>
                <NCard size="small" title="Shopping List">
                  <p v-for="item in shoppingList" :key="item.item">
                    {{ item.quantity }}x {{ item.item }}
                  </p>
                </NCard>
              </NGridItem>
              <NGridItem>
                <NCard size="small" title="Excess Items">
                  <p v-for="item in shoppingList" :key="item.item">
                    {{ (item.quantity - item.used).toFixed(2) }}{{item.unit}} of {{ item.item }}
                  </p>
                </NCard>
              </NGridItem>
              <NGridItem>
                <NCard size="small" title="Cost">
                  <p class="text-red-600 text-lg">£{{ shoppingList.reduce((a,b) => a + (b.quantity * (shopItemStore.getPrice(b.item) ?? 0)), 0)}}</p>
                </NCard>
              </NGridItem>
            </NGrid>
          </NGridItem>
        </NGrid>
      </NLayout>
    </NMessageProvider>
  </NConfigProvider>
</template>

<script setup lang="ts">
import {
  NConfigProvider,
  darkTheme,
  GlobalTheme,
  NMessageProvider,
  NCard,
  NGrid,
  NLayout,
  NGridItem,
  NTable,
  NButton,
  NModal,
} from "naive-ui";
import { onBeforeMount, ref } from "vue";

import MealSelector from "@/components/MealSelector.vue";

import { useRecipeStore } from "./store/recipeStore";
import { useShopItemStore } from "./store/shopItemStore";

import { useMealStore } from "@/store/mealStore";
import { IDayPlan } from "@/types/IDayPlan";
import { IIngredient } from "@/types/IIngredient";
import { IShoppingListItem } from "@/types/IShoppingListItem";
import { IWeekPlan } from "@/types/IWeekPlan";
import { computed } from "vue";
import configureMeasurements, {
  allMeasures,
  AllMeasuresUnits,
} from "convert-units";
import { Unit } from "@/types/Unit";

const theme = ref<GlobalTheme | null>(darkTheme);

const recipeStore = useRecipeStore();
const shopItemStore = useShopItemStore();

onBeforeMount(async () => {
  await recipeStore.getRecipes();
  await shopItemStore.getItems();
});

const convert = configureMeasurements(allMeasures);

const mealStore = useMealStore();
const recipes = computed<{
  [name: string]: number;
}>(() => {
  const dayKeys = [
    "saturday",
    "sunday",
    "monday",
    "tuesday",
    "wednesday",
    "thursday",
    "friday",
  ] as (keyof IWeekPlan)[];
  const mealKeys = ["lunch", "dinner"] as (keyof IDayPlan)[];

  const res: {
    [name: string]: number;
  } = {};
  for (const day of dayKeys) {
    for (const meal of mealKeys) {
      const recipe = mealStore.plan[day][meal];
      if (!recipe) continue;
      const recipeName = recipeStore.recipeNames[recipe];
      if (res[recipeName]) res[recipeName]++;
      else res[recipeName] = 1;
    }
  }
  return res;
});

const shoppingList = computed<IShoppingListItem[]>(() => {
  const recipeKeys = Object.keys(recipes.value);
  const allRecipeIngredients: { [name: string]: IIngredient } = Object.assign(
    {},
    ...recipeStore.recipes
      .filter((x) => recipeKeys.includes(x.name))
      .map((x) => x.ingredients)
      .flat()
      .map((x) => ({ [x.item!.name]: x }))
  );

  const ingredients: { [name: string]: IShoppingListItem } = {}; // value is the quantity converted to the base unit of the ingredient

  for (const key of recipeKeys) {
    const recipe = recipeStore.byName(key);
    if (!recipe?.ingredients) continue;
    for (const ingredient of recipe!.ingredients) {
      const fromUnit = Unit[ingredient.unit!].toString().toLocaleLowerCase();
      const toUnit = Unit[ingredient.item!.unit].toString().toLocaleLowerCase();
      const convertedQty = convert(ingredient.quantity! * recipes.value[key])
        .from(fromUnit as unknown as AllMeasuresUnits)
        .to(toUnit as unknown as AllMeasuresUnits);
      if (ingredients[ingredient.item!.name]) {
        ingredients[ingredient.item!.name].quantity += convertedQty;
      } else {
        ingredients[ingredient.item!.name] = {
          item: ingredient.item!.name,
          used: convertedQty,
          quantity: Math.ceil(
            convertedQty / allRecipeIngredients[ingredient.item!.name].item?.quantity!
          ),
          unit: toUnit
        };
      }
    }
  }

  return Object.values(ingredients)
});
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}
</style>
