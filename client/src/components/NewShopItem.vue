<template>
    <div class="flex flex-col gap-y-2">
        <NInputGroup class="w-full">
            <NInputGroupLabel class="w-1/2">Item name:</NInputGroupLabel>
            <NInput class="!w-1/2" placeholder="Name" v-model:value="newShopItem.name" />
        </NInputGroup>
        <NInputGroup class="w-full">
            <NInputGroupLabel class="w-1/2">Quantity:</NInputGroupLabel>
            <NInputNumber class="w-1/2" v-model:value="newShopItem.quantity" placeholder="Quantity" />
        </NInputGroup>
        <NInputGroup class="w-full">
            <NInputGroupLabel class="w-1/2">Unit:</NInputGroupLabel>
            <NSelect class="w-1/2" :options="unitOptions" v-model:value="newShopItem.unit" />
        </NInputGroup>
        <NInputGroup class="w-full">
            <NInputGroupLabel class="w-1/2">Price £</NInputGroupLabel>
            <NInputNumber class="w-1/2" v-model:value="newShopItem.price" placeholder="Price" />
        </NInputGroup>
        <div class="flex justify-end">
        <NButton @click="save">Save</NButton>
        </div>
    </div>
</template>

<script setup lang="ts">
import { useShopItemStore } from '@/store/shopItemStore';
import { IShopItem } from '@/types/IShopItem';
import { Unit } from '@/types/Unit';
import { NInputGroup, NInput, NInputNumber, NSelect, NInputGroupLabel, NButton } from 'naive-ui';
import { SelectMixedOption } from 'naive-ui/es/select/src/interface';
import { ref } from 'vue';

const unitOptions = Object.values(Unit).filter(x => typeof x == 'number').map<SelectMixedOption>(x => ({
    label: Unit[x as number],
    value: x
}))

const emptyShopItem: IShopItem = {
    id: 0,
    name: "",
    quantity: 0,
    price: 0,
    unit: Unit.G
};

const newShopItem = ref<IShopItem>(Object.assign({}, emptyShopItem));
const shopItemStore = useShopItemStore();
const emit = defineEmits(["saved"]);
const save = async () => {
    await shopItemStore.create(newShopItem.value);
    newShopItem.value = Object.assign({}, emptyShopItem)
    emit("saved")
}
</script>