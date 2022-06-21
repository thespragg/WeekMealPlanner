<template>
    <div>
        <NButton v-if="showButton" @click="emit('select', { day: day, meal: meal })">Choose
            Recipe</NButton>
        <p v-else>{{ text }}</p>
    </div>
</template>

<script lang="ts" setup>
import { useMealStore } from '@/store/mealStore';
import { IDayPlan } from '@/types/IDayPlan';
import { IWeekPlan } from '@/types/IWeekPlan';
import { storeToRefs } from 'pinia';
import { computed } from 'vue';
import { NButton } from 'naive-ui';
import { useRecipeStore } from '@/store/recipeStore';

const { day, meal } = defineProps<{ day: keyof IWeekPlan, meal: keyof IDayPlan }>();
const { plan } = storeToRefs(useMealStore());
const recipeStore = useRecipeStore();

const emit = defineEmits(["select"])

const showButton = computed(() => !plan.value[day][meal])
const text = computed(() => recipeStore.recipeNames![plan.value[day][meal]!])
</script>t