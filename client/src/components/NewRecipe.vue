<template>
    <NCard title="New recipe" class="w-full">
        <NInputGroup class="w-full">
            <NInputGroupLabel class="w-1/3">Recipe Name:</NInputGroupLabel>
            <NInput class="w-2/3" v-model:value="newRecipe.name" placeholder="Name" />
        </NInputGroup>
        <div class="my-4 p-2 border border-solid border-neutral-600">
            <p class="font-bold mb-2">Ingredients</p>
            <div v-for="(ingredient, i) in newRecipe.ingredients" :key="i" class="flex">
                <p>{{ ingredient.quantity }}{{ Unit[ingredient.unit as number] }} {{ ingredient.item!.name }}</p>
            </div>

            <NInputGroup v-if="showAddIngredient">
                <NSelect class="w-2/4" :options="shopItemStore.shopItemSelections" v-model:value="ingredientShopItem" />
                <NInputNumber class="w-1/4" v-model:value="newIngredient.quantity" />
                <NSelect class="w-1/4" :options="unitOptions" v-model:value="newIngredient.unit" />
                <NButton @click="addIngredient">Save</NButton>
            </NInputGroup>

            <div class="flex justify-end mt-4">
                <NButton class="mr-4" @click="showAddIngredient = !showAddIngredient"> {{ addIngredientText }}
                </NButton>
            </div>
        </div>

        <NButton @click="saveRecipe">Save recipe</NButton>
    </NCard>
</template>

<script setup lang="ts">
import { ingredients } from "@/api/ingredients";
import { useRecipeStore } from "@/store/recipeStore";
import { useShopItemStore } from "@/store/shopItemStore";
import { IIngredient } from "@/types/IIngredient";
import { IRecipe } from "@/types/IRecipe";
import { Unit } from "@/types/Unit";
import { NCard, NInput, NInputGroupLabel, NInputGroup, NButton, NSelect, NInputNumber } from "naive-ui"
import { SelectMixedOption } from "naive-ui/es/select/src/interface";
import { computed, ref } from "vue";

const shopItemStore = useShopItemStore();

const addIngredientText = computed(() => !showAddIngredient.value ? 'Add Ingredient' : 'Cancel');
const newRecipe = ref<IRecipe>({
    id: 0,
    name: "",
    ingredients: []
});

const ingredientShopItem = ref<number | null>(null);
const newIngredient = ref<IIngredient>({
    id: 0,
    item: null,
    quantity: null,
    unit: null
});

const showAddIngredient = ref(false);
const unitOptions = Object.values(Unit).filter(x => typeof x == 'number').map<SelectMixedOption>(x => ({
    label: Unit[x as number],
    value: x
}))

const addIngredient = async () => {
    newIngredient.value.item = shopItemStore.shop.find(x => x.id == ingredientShopItem.value)!;
    ingredientShopItem.value = null;
    const ingredient = await ingredients.create(newIngredient.value);
    newRecipe.value.ingredients.push(ingredient);
    newIngredient.value = {
        id: 0,
        item: null,
        quantity: null,
        unit: null
    }
    showAddIngredient.value = false;
}

const recipeStore = useRecipeStore();
const emit = defineEmits(["close"])
const saveRecipe = async () => {
    recipeStore.create(newRecipe.value);
    emit("close")
}
</script>