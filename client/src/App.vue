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
                      <MealSelector day="saturday" meal="lunch" @select="toggleRecipeModal($event.day, $event.meal)" />
                    </td>
                    <td>
                      <MealSelector day="saturday" meal="dinner" @select="toggleRecipeModal($event.day, $event.meal)" />
                    </td>
                  </tr>
                  <tr>
                    <td>Sunday</td>
                    <td>
                      <MealSelector day="sunday" meal="lunch" @select="toggleRecipeModal($event.day, $event.meal)" />
                    </td>
                    <td>
                      <MealSelector day="sunday" meal="dinner" @select="toggleRecipeModal($event.day, $event.meal)" />
                    </td>
                  </tr>
                  <tr>
                    <td>Monday</td>
                    <td>
                      <MealSelector day="monday" meal="lunch" @select="toggleRecipeModal($event.day, $event.meal)" />
                    </td>
                    <td>
                      <MealSelector day="monday" meal="dinner" @select="toggleRecipeModal($event.day, $event.meal)" />
                    </td>
                  </tr>
                  <tr>
                    <td>Tuesday</td>
                    <td>
                      <MealSelector day="tuesday" meal="lunch" @select="toggleRecipeModal($event.day, $event.meal)" />
                    </td>
                    <td>
                      <MealSelector day="tuesday" meal="dinner" @select="toggleRecipeModal($event.day, $event.meal)" />
                    </td>
                  </tr>
                  <tr>
                    <td>Wednesday</td>
                    <td>
                      <MealSelector day="wednesday" meal="lunch" @select="toggleRecipeModal($event.day, $event.meal)" />
                    </td>
                    <td>
                      <MealSelector day="wednesday" meal="dinner"
                        @select="toggleRecipeModal($event.day, $event.meal)" />
                    </td>
                  </tr>
                  <tr>
                    <td>Thursday</td>
                    <td>
                      <MealSelector day="thursday" meal="lunch" @select="toggleRecipeModal($event.day, $event.meal)" />
                    </td>
                    <td>
                      <MealSelector day="thursday" meal="dinner" @select="toggleRecipeModal($event.day, $event.meal)" />
                    </td>
                  </tr>
                  <tr>
                    <td>Friday</td>
                    <td>
                      <MealSelector day="friday" meal="lunch" @select="toggleRecipeModal($event.day, $event.meal)" />
                    </td>
                    <td>
                      <MealSelector day="friday" meal="dinner" @select="toggleRecipeModal($event.day, $event.meal)" />
                    </td>
                  </tr>
                </tbody>
              </NTable>
            </NCard>
          </NGridItem>
          <NGridItem span="1">
            <NGrid cols="1" y-gap="6">
              <NGridItem>
                <ShoppingList :recipes="chosenRecipes" />
              </NGridItem>
              <NGridItem>
                <NCard size="small" title="Excess Items"></NCard>
              </NGridItem>
              <NGridItem>
                <NCard size="small" title="Cost"></NCard>
              </NGridItem>
            </NGrid>
          </NGridItem>
        </NGrid>
        <NModal v-if="!showNewRecipe" v-model:show="showRecipeModal" @after-leave="closeItems">
          <NCard :title="`Choose Recipe for ${capitalise(choosingDay)} ${capitalise(choosingMeal)}`" class="w-2/3">
            <div @click="selectRecipe(Number(key))"
              class="cursor-pointer hover:!bg-gray-800 py-3 px-4 border border-solid border-slate-600 flex justify-between"
              v-for="key in Object.keys(recipeList!)">
              <p> {{ recipeList![Number(key) as
                  keyof IRecipeList]
              }} </p>
              <p> edit </p>
            </div>
            <div class="flex justify-end mt-4">
              <NButton @click="showNewRecipe = true">New Recipe</NButton>
            </div>
          </NCard>
        </NModal>
        <NModal v-else v-model:show="showRecipeModal" @after-leave="closeItems">
          <NewRecipe @close="showNewRecipe = false" />
        </NModal>
      </NLayout>
    </NMessageProvider>
  </NConfigProvider>
</template>

<script setup lang="ts">
import { NConfigProvider, darkTheme, GlobalTheme, NMessageProvider, NCard, NGrid, NLayout, NGridItem, NTable, NButton, NModal } from "naive-ui"
import MealSelector from "@/components/MealSelector.vue";

import NewRecipe from "@/components/NewRecipe.vue"
import { onBeforeMount, ref } from "vue";
import { IWeekPlan } from "@/types/IWeekPlan";
import { recipes } from "@/api/recipes"
import { IRecipeList } from "./types/IRecipeList";
import { IDayPlan } from "./types/IDayPlan";
import { IRecipe } from "./types/IRecipe";
import ShoppingList from "@/components/ShoppingList.vue"

import { useMealStore } from "@/store/mealStore"
import { useRecipeStore } from "./store/recipeStore";

const theme = ref<GlobalTheme | null>(darkTheme);

const mealStore = useMealStore();
const recipeStore = useRecipeStore();

const recipeList = ref<IRecipeList | null>(null);

onBeforeMount(async () => {
  await recipeStore.getRecipes();
  recipeList.value = await recipes.names();
})

const showRecipeModal = ref(false);
const showNewRecipe = ref(false);
const choosingDay = ref("");
const choosingMeal = ref("");
const toggleRecipeModal = (day: string, meal: string) => {
  choosingDay.value = day;
  choosingMeal.value = meal;
  showRecipeModal.value = true;
}

const chosenRecipes = ref<IRecipe[]>([]);
const selectRecipe = async (key: number) => {
  mealStore.plan[choosingDay.value as keyof IWeekPlan][choosingMeal.value as keyof IDayPlan] = key;
  choosingDay.value = "";
  choosingMeal.value = "";
  showRecipeModal.value = false;
  await updateRecipes();
}

const updateRecipes = async () => {
  chosenRecipes.value = []
  for (const dayKey of Object.keys(mealStore.plan)) {
    const day = mealStore.plan[dayKey as keyof IWeekPlan];
    for (const key of Object.keys(day)) {
      const recipe = day[key as keyof IDayPlan];
      if (!recipe) continue;
      chosenRecipes.value.push(await recipes.getById(recipe));
    }
  }
}

const closeItems = () => {
  showNewRecipe.value = false;
}

// HELPERS //
const capitalise = (s: string) => (s && s[0].toUpperCase() + s.slice(1)) || "";
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
