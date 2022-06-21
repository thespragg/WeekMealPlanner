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
                <ShoppingList />
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
      </NLayout>
    </NMessageProvider>
  </NConfigProvider>
</template>

<script setup lang="ts">
import { NConfigProvider, darkTheme, GlobalTheme, NMessageProvider, NCard, NGrid, NLayout, NGridItem, NTable, NButton, NModal } from "naive-ui"
import { onBeforeMount, ref } from "vue";

import ShoppingList from "@/components/ShoppingList.vue";
import MealSelector from "@/components/MealSelector.vue";

import { useRecipeStore } from "./store/recipeStore";
import { useShopItemStore } from "./store/shopItemStore";

const theme = ref<GlobalTheme | null>(darkTheme);

const recipeStore = useRecipeStore();
const shopItemStore = useShopItemStore();

onBeforeMount(async () => {
  await recipeStore.getRecipes();
  await shopItemStore.getItems();
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
