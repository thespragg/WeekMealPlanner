<template>
    <NCard title="New recipe" class="w-2/3">
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
                <NSelect class="w-2/4" :options="shopItemOptions" v-model:value="ingredientShopItem"/>
                <NInputNumber class="w-1/4" v-model:value="newIngredient.quantity" />
                <NSelect class="w-1/4" :options="unitOptions" v-model:value="newIngredient.unit"/>
                <NButton @click="addIngredient">Save</NButton>
            </NInputGroup>

            <div class="flex justify-end mt-4">
                <NButton class="mr-4" @click="showAddIngredient = !showAddIngredient"> {{ addIngredientText }}
                </NButton>
                <NButton @click.stop="showAddShopItem = !showAddShopItem">{{ addShopItemText }}</NButton>
            </div>

            <NInputGroup v-if="showAddShopItem" class="mt-4">
                <NInput v-model:value="newShopItem.name" class="w-1/6" placeholder="Name" />
                <NInputNumber v-model:value="newShopItem.quantity" class="w-2/6" placeholder="Quantity" />
                <NSelect class="w-2/6" :options="unitOptions" v-model:value="newShopItem.unit" />
                <NInputGroupLabel>£</NInputGroupLabel>
                <NInputNumber v-model:value="newShopItem.price" class="w-2/6" placeholder="Price" />
                <NButton @click="saveNewShopItem">Save</NButton>
            </NInputGroup>
        </div>

        <NButton @click="saveRecipe">Save recipe</NButton>
    </NCard>
</template>

<script setup lang="ts">
import { ingredients } from "@/api/ingredients";
import { recipes } from "@/api/recipes";
import { shop } from "@/api/shop";
import { IIngredient } from "@/types/IIngredient";
import { IRecipe } from "@/types/IRecipe";
import { IShopItem } from "@/types/IShopItem";
import { Unit } from "@/types/Unit";
import { NCard, NInput, NInputGroupLabel, NInputGroup, NButton, NSelect, NInputNumber } from "naive-ui"
import { SelectMixedOption } from "naive-ui/es/select/src/interface";
import { computed, onBeforeMount, ref } from "vue";
import NewShopItem from "./NewShopItem.vue"

const addIngredientText = computed(() => !showAddIngredient.value ? 'Add Ingredient' : 'Cancel');
const addShopItemText = computed(() => !showAddShopItem.value ? 'New Shop Item' : 'Cancel ');

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

const newShopItem = ref<IShopItem>({
    id: 0,
    name: "",
    quantity: 0,
    price: 0,
    unit: Unit.G
})

const showAddShopItem = ref(false);
const shopItems = ref<IShopItem[]>([]);
const shopItemOptions = computed<SelectMixedOption[]>(() => shopItems.value.map(x => ({
    label: x.name,
    value: x.id
})))
const showAddIngredient = ref(false);
const unitOptions = Object.values(Unit).filter(x => typeof x == 'number').map<SelectMixedOption>(x => ({
    label: Unit[x as number],
    value: x
}))

const addIngredient = async () => {
    newIngredient.value.item = shopItems.value.find(x=>x.id == ingredientShopItem.value)!;
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

const saveNewShopItem = async () => {
    const shopItem = await shop.create(newShopItem.value);
    newShopItem.value = {
        id: 0,
        name: "",
        quantity: 0,
        price: 0,
        unit: Unit.G
    }
    showAddShopItem.value = false;
    shopItems.value.push(shopItem);
}

onBeforeMount(async () => {
    shopItems.value = (await shop.get())
})

const emit = defineEmits(["close"])
const saveRecipe = async () => {
    await recipes.create(newRecipe.value);
    emit("close")
}
</script>