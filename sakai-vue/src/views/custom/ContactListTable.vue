<script setup>
import { onMounted, ref } from 'vue';
import { fetchContacts } from "@/API/contactBookAPI";
import { FilterMatchMode, FilterOperator } from '@primevue/core/api';

//const router = useRouter();

const contacts = ref(null);
const filters = ref({});
const loading = ref(null);
const error = ref(null);

const loadContacts = async () => {
  loading.value = true;
  error.value = null;
  
  try {
    const response = await fetchContacts();
    contacts.value = response.data;
  } catch (err) {
    error.value = err;
    console.error('Failed to fetch contacts:', err);
  } finally {
    loading.value = false;
  }
};

const refreshContacts = async () => {
  await loadContacts();
};

const openContactDetails = (contactId) => {
  router.push({ name: "Details", params: { contactId } });
};

const addContact = () => {
  // Add Contact Logic goes here later
  console.log('Add contact clicked');
};

onMounted(async () => {
  await loadContacts();

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
        
    <!-- Error State -->
    <div v-if="error" class="error-message">
      <p>An error occurred: {{ error.message }}</p>
      <Button 
        label="Refresh" 
        icon="pi pi-refresh" 
        @click="refreshContacts" 
        class="p-button-success" 
      />
    </div>

    <!-- Loading State -->
    <div v-else-if="loading" class="loading-message">
      <h3>Loading...</h3>
    </div>
        <DataTable 
            v-else
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
                        <Button icon="pi pi-pencil" type="button" class="p-button-text" @click="openContactDetails(slotProps.data.id)"></Button>
                        <Button icon="pi pi-trash" type="button" class="p-button-text" @click="openContactDetails(slotProps.data.id)"></Button>
                    </div>
                </template>
            </Column>
        </DataTable>
    </div>
</template>