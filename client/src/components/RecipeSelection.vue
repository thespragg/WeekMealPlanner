<template>
    <div @click="emit('select', name)"
        class="cursor-pointer hover:!bg-neutral-800 py-3 px-4 border border-solid border-slate-600 flex items-center justify-between mb-2"
        v-for="name in recipeStore.recipeNames">
        <p> {{ name }} </p>
        <font-awesome-icon @click.stop="" :icon="['fas', 'pencil']" />
    </div>
</template>

<script lang="ts" setup>
import { useRecipeStore } from '@/store/recipeStore';
const emit = defineEmits(["select"]);
const recipeStore = useRecipeStore();
</script>