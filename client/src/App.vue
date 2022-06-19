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
                      <NButton v-if="!mealPlan.saturday.lunch" @click="toggleRecipeModal('saturday', 'lunch')">Choose
                        Recipe</NButton>
                      <p v-else>{{ recipeList![mealPlan.saturday.lunch] }}</p>
                    </td>
                    <td>
                      <NButton v-if="!mealPlan.saturday.dinner" @click="toggleRecipeModal('saturday', 'dinner')">Choose
                        Recipe</NButton>
                      <p v-else>{{ recipeList![mealPlan.saturday.dinner] }}</p>
                    </td>
                  </tr>
                  <tr>
                    <td>Sunday</td>
                    <td>
                      <NButton v-if="!mealPlan.sunday.lunch" @click="toggleRecipeModal('sunday', 'lunch')">Choose Recipe
                      </NButton>
                      <p v-else>{{ recipeList![mealPlan.sunday.lunch] }}</p>
                    </td>
                    <td>
                      <NButton v-if="!mealPlan.sunday.dinner" @click="toggleRecipeModal('sunday', 'dinner')">Choose
                        Recipe</NButton>
                      <p v-else>{{ recipeList![mealPlan.sunday.dinner] }}</p>
                    </td>
                  </tr>
                  <tr>
                    <td>Monday</td>
                    <td>
                      <NButton v-if="!mealPlan.monday.lunch" @click="toggleRecipeModal('monday', 'lunch')">Choose Recipe
                      </NButton>
                      <p v-else>{{ recipeList![mealPlan.monday.lunch] }}</p>
                    </td>
                    <td>
                      <NButton v-if="!mealPlan.monday.dinner" @click="toggleRecipeModal('monday', 'dinner')">Choose
                        Recipe</NButton>
                      <p v-else>{{ recipeList![mealPlan.monday.dinner] }}</p>
                    </td>
                  </tr>
                  <tr>
                    <td>Tuesday</td>
                    <td>
                      <NButton v-if="!mealPlan.tuesday.lunch" @click="toggleRecipeModal('tuesday', 'lunch')">Choose
                        Recipe</NButton>
                      <p v-else>{{ recipeList![mealPlan.tuesday.lunch] }}</p>
                    </td>
                    <td>
                      <NButton v-if="!mealPlan.tuesday.dinner" @click="toggleRecipeModal('tuesday', 'dinner')">Choose
                        Recipe</NButton>
                      <p v-else>{{ recipeList![mealPlan.tuesday.dinner] }}</p>
                    </td>
                  </tr>
                  <tr>
                    <td>Wednesday</td>
                    <td>
                      <NButton v-if="!mealPlan.wednesday.lunch" @click="toggleRecipeModal('wednesday', 'lunch')">Choose
                        Recipe</NButton>
                      <p v-else>{{ recipeList![mealPlan.wednesday.lunch] }}</p>
                    </td>
                    <td>
                      <NButton v-if="!mealPlan.wednesday.dinner" @click="toggleRecipeModal('wednesday', 'dinner')">
                        Choose Recipe</NButton>
                      <p v-else>{{ recipeList![mealPlan.wednesday.dinner] }}</p>
                    </td>
                  </tr>
                  <tr>
                    <td>Thursday</td>
                    <td>
                      <NButton v-if="!mealPlan.thursday.lunch" @click="toggleRecipeModal('thursday', 'lunch')">Choose
                        Recipe</NButton>
                      <p v-else>{{ recipeList![mealPlan.thursday.lunch] }}</p>
                    </td>
                    <td>
                      <NButton v-if="!mealPlan.thursday.dinner" @click="toggleRecipeModal('thursday', 'dinner')">Choose
                        Recipe</NButton>
                      <p v-else>{{ recipeList![mealPlan.thursday.dinner] }}</p>
                    </td>
                  </tr>
                  <tr>
                    <td>Friday</td>
                    <td>
                      <NButton v-if="!mealPlan.friday.lunch" @click="toggleRecipeModal('friday', 'lunch')">Choose Recipe
                      </NButton>
                      <p v-else>{{ recipeList![mealPlan.friday.lunch] }}</p>
                    </td>
                    <td>
                      <NButton v-if="!mealPlan.friday.dinner" @click="toggleRecipeModal('friday', 'dinner')">Choose
                        Recipe</NButton>
                      <p v-else>{{ recipeList![mealPlan.friday.dinner] }}</p>
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
import NewRecipe from "@/components/NewRecipe.vue"
import { onBeforeMount, ref } from "vue";
import { IWeekPlan } from "@/types/IWeekPlan";
import { recipes } from "@/api/recipes"
import { IRecipeList } from "./types/IRecipeList";
import { IDayPlan } from "./types/IDayPlan";
import { IRecipe } from "./types/IRecipe";
import ShoppingList from "@/components/ShoppingList.vue"

const theme = ref<GlobalTheme | null>(darkTheme);

const mealPlan = ref<IWeekPlan>({
  monday: {},
  tuesday: {},
  wednesday: {},
  thursday: {},
  friday: {},
  saturday: {},
  sunday: {}
})

const recipeList = ref<IRecipeList | null>(null);

onBeforeMount(async () => {
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
  mealPlan.value[choosingDay.value as keyof IWeekPlan][choosingMeal.value as keyof IDayPlan] = key;
  choosingDay.value = "";
  choosingMeal.value = "";
  showRecipeModal.value = false;
  await updateRecipes();
}

const updateRecipes = async () => {
  chosenRecipes.value = []
  for (const dayKey of Object.keys(mealPlan.value)) {
    const day = mealPlan.value[dayKey as keyof IWeekPlan];
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
