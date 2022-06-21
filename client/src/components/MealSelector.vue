<template>
    <div>
        <NButton v-if="showButton" @click="showModal = true">Choose
            Recipe</NButton>
        <p v-else>{{ text }}</p>
        <NModal v-model:show="showModal">
            <NCard class="w-2/3" :title="modalTitle">
                <RecipeSelection @select="selectMeal" v-if="modalState == 0" />
                <NewRecipe @close="modalState = 0" v-else-if="modalState == 1" />
                <NewShopItem @saved="modalState = 0" v-else-if="modalState == 2" />
                <template #action>
                    <div class="flex w-full justify-end">
                        <NButton class="mr-4" @click="modalState = 1">New recipe</NButton>
                        <NButton class="mr-4" @click="modalState = 2">New shop item</NButton>
                        <NButton @click="modalState = 0" v-if="modalState == 1 || modalState == 2">Cancel</NButton>
                    </div>
                </template>
            </NCard>
        </NModal>
    </div>
</template>

<script lang="ts" setup>
import { useMealStore } from '@/store/mealStore';
import { IDayPlan } from '@/types/IDayPlan';
import { IWeekPlan } from '@/types/IWeekPlan';
import { storeToRefs } from 'pinia';
import { computed, ref } from 'vue';
import { NButton, NModal, NCard } from 'naive-ui';
import { useRecipeStore } from '@/store/recipeStore';

import NewShopItem from './NewShopItem.vue';
import NewRecipe from "./NewRecipe.vue";
import RecipeSelection from "./RecipeSelection.vue"

const { day, meal } = defineProps<{ day: keyof IWeekPlan, meal: keyof IDayPlan }>();
const { plan } = storeToRefs(useMealStore());
const recipeStore = useRecipeStore();

const showModal = ref(false);
const modalTitle = computed(() => {
    switch (modalState.value) {
        case 0:
            return "Select Recipe";
        case 1:
            return "New Recipe";
        case 2:
            return "New Shop Item";
    }
})
const modalState = ref(0);

const showButton = computed(() => !plan.value[day][meal])
const text = computed(() => recipeStore.recipeNames![plan.value[day][meal]!])

const selectMeal = (recipe: string) => {
    const keys = Object.keys(recipeStore.recipeNames).map(x => Number(x));
    const recipeId = keys.find((x: number) => recipeStore.recipeNames[x] == recipe);
    plan.value[day][meal] = recipeId!;
    showModal.value = false;
}
</script>t