<template>
    <NCard size="small" title="Shopping List">
        <p v-for="item in shoppingList" :key="item.item.name">{{ item.quantity }}x {{ item.item.name }}</p>
    </NCard>
</template>

<script setup lang="ts">
import { shop } from '@/api/shop';
import { IRecipe } from '@/types/IRecipe';
import { IShopItem } from '@/types/IShopItem';
import { IShoppingListItem } from '@/types/IShoppingListItem';
import { NCard } from "naive-ui"
import { computed, onBeforeMount, onUpdated, ref } from 'vue';

const props = defineProps<{ recipes: IRecipe[] }>();

const shoppingList = ref<IShoppingListItem[]>([]);
const calculateShoppingList = () => {
    const allIngredients = props.recipes.map(x => x.ingredients).flat();
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