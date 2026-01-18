<script setup>
import { ContactService } from '@/service/ContactService';
import { onMounted, ref } from 'vue';

const contacts = ref(null);

onMounted(() => {
    ContactService.getContactsSmallTest().then((data) => contacts.value = data);
});
</script>

<template>
    <div class="card">
        <DataTable :value="contacts" :rows="5" :paginator="true" v-model:filters="filters" responsiveLayout="scroll" :loading="loading1" :filters="filters" :globalFilterFields="['name', 'email', 'phone']"">
            <template #header>
                <div class="flex justify-between">
                    <Button type="button" icon="pi pi-plus" label="Add" outlined @click="addContact()" />
                    <IconField>
                        <InputIcon>
                            <i class="pi pi-search" />
                        </InputIcon>
                        <!-- <InputText v-model="filters['global'].value" placeholder="Keyword Search" /> -->
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
