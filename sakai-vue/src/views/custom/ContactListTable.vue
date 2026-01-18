<script setup>
import { ContactService } from '@/service/ContactService';
import { onMounted, ref } from 'vue';
import { FilterMatchMode, FilterOperator } from '@primevue/core/api';

const contacts = ref(null);
const filters = ref({});
const loading = ref(null);

onMounted(() => {
    ContactService.getContactsSmallTest().then((data) => contacts.value = data);

    initFilters();
});

function initFilters() {
    filters.value = {
        global: { value: null, matchMode: FilterMatchMode.CONTAINS },
        name: { operator: FilterOperator.AND, constraints: [{ value: null, matchMode: FilterMatchMode.STARTS_WITH }] },
        email: { operator: FilterOperator.AND, constraints: [{ value: null, matchMode: FilterMatchMode.STARTS_WITH }] },
        phone: { operator: FilterOperator.AND, constraints: [{ value: null, matchMode: FilterMatchMode.STARTS_WITH }] },
    };
}
</script>

<template>
    <div class="card">
        <DataTable 
            :value="contacts"
            :rows="5"
            :paginator="true"
            v-model:filters="filters"
            responsiveLayout="scroll"
            :loading="loading"
            :globalFilterFields="['name', 'email', 'phone']"
        >
            <template #header>
                <div class="flex justify-between">
                    <Button type="button" icon="pi pi-plus" label="Add" outlined @click="addContact()" />
                    <IconField>
                        <InputIcon>
                            <i class="pi pi-search" />
                        </InputIcon>
                        <InputText v-if="filters['global']" v-model="filters['global'].value" placeholder="Keyword Search" />
                    </IconField>
                </div>
            </template>
            <Column field="name" header="Name" :sortable="true" style="width: 35%"></Column>
            <Column field="email" header="Email" :sortable="true" style="width: 35%">
                <template #body="slotProps">
                    {{ slotProps.data.email }}
                </template>
            </Column>
            <Column field="phone" header="Phone" :sortable="true" style="width: 35%">
                <template #body="slotProps">
                    {{ slotProps.data.phone }}
                </template>
            </Column>
            <Column style="width: 15%" header="Options">
                <template #body>
                    <div class="flex items-center gap-2">
                        <Button icon="pi pi-pencil" type="button" class="p-button-text"></Button>
                        <Button icon="pi pi-trash" type="button" class="p-button-text"></Button>
                    </div>
                </template>
            </Column>
        </DataTable>
    </div>
</template>
