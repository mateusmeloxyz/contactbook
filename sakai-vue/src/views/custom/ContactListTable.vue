<script setup>
import { onMounted, ref, reactive } from 'vue';
import { fetchContacts, createContact, updateContact, deleteContact } from "@/API/contactBookAPI";
import { FilterMatchMode, FilterOperator } from '@primevue/core/api';
import { useToast } from 'primevue/usetoast';

const toast = useToast();

// State
const contacts = ref([]);
const filters = ref({});
const loading = ref(false);
const error = ref(null);
const displayDialog = ref(false);
const displayDeleteDialog = ref(false);
const isEditMode = ref(false);
const selectedContactId = ref(null);

// Form model
const contactForm = reactive({
  id: null,
  name: '',
  email: '',
  phone: '',
  isActive: true
});

const loadContacts = async () => {
  loading.value = true;
  error.value = null;
  
  try {
    const response = await fetchContacts();
    contacts.value = response.data;
  } catch (err) {
    error.value = err;
    console.error('Failed to fetch contacts:', err);
    toast.add({
      severity: 'error',
      summary: 'Error',
      detail: 'Failed to load contacts',
      life: 3000
    });
  } finally {
    loading.value = false;
  }
};

const handleCreateContact = async () => {
  try {
    loading.value = true;
    
    // Validate required fields
    if (!contactForm.name || !contactForm.email || !contactForm.phone) {
      toast.add({
        severity: 'warn',
        summary: 'Warning',
        detail: 'Please fill in all required fields',
        life: 3000
      });
      return;
    }
    
    await createContact(contactForm);
    
    toast.add({
      severity: 'success',
      summary: 'Success',
      detail: 'Contact created successfully',
      life: 3000
    });
    
    displayDialog.value = false;
    resetForm();
    await loadContacts(); // Refresh the list
  } catch (err) {
    console.error('Failed to create contact:', err);
    toast.add({
      severity: 'error',
      summary: 'Error',
      detail: 'Failed to create contact',
      life: 3000
    });
  } finally {
    loading.value = false;
  }
};

// Update Contact
const handleUpdateContact = async () => {
  try {
    loading.value = true;
    
    // Validate required fields
    if (!contactForm.name || !contactForm.email || !contactForm.phone) {
      toast.add({
        severity: 'warn',
        summary: 'Warning',
        detail: 'Please fill in all required fields',
        life: 3000
      });
      return;
    }
    
    await updateContact(contactForm.id, contactForm);
    
    toast.add({
      severity: 'success',
      summary: 'Success',
      detail: 'Contact updated successfully',
      life: 3000
    });
    
    displayDialog.value = false;
    resetForm();
    await loadContacts(); // Refresh the list
  } catch (err) {
    console.error('Failed to update contact:', err);
    toast.add({
      severity: 'error',
      summary: 'Error',
      detail: 'Failed to update contact',
      life: 3000
    });
  } finally {
    loading.value = false;
  }
};

// Delete Contact
const handleDeleteContact = async () => {
  try {
    loading.value = true;
    
    await deleteContact(selectedContactId.value);
    
    toast.add({
      severity: 'success',
      summary: 'Success',
      detail: 'Contact deleted successfully',
      life: 3000
    });
    
    displayDeleteDialog.value = false;
    selectedContactId.value = null;
    await loadContacts(); // Refresh the list
  } catch (err) {
    console.error('Failed to delete contact:', err);
    toast.add({
      severity: 'error',
      summary: 'Error',
      detail: 'Failed to delete contact',
      life: 3000
    });
  } finally {
    loading.value = false;
  }
};

// UI Handlers
const addContact = () => {
  resetForm();
  isEditMode.value = false;
  displayDialog.value = true;
};

const editContact = (contact) => {
  // Populate form with contact data
  contactForm.id = contact.id;
  contactForm.name = contact.name;
  contactForm.email = contact.email;
  contactForm.phone = contact.phone;
  contactForm.isActive = contact.isActive !== undefined ? contact.isActive : true;
  
  isEditMode.value = true;
  displayDialog.value = true;
};

const confirmDelete = (id) => {
  selectedContactId.value = id;
  displayDeleteDialog.value = true;
};

const submitForm = () => {
  if (isEditMode.value) {
    handleUpdateContact();
  } else {
    handleCreateContact();
  }
};

const resetForm = () => {
  contactForm.id = null;
  contactForm.name = '';
  contactForm.email = '';
  contactForm.phone = '';
  contactForm.isActive = true;
};

const refreshContacts = async () => {
  await loadContacts();
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
    <div v-if="error" class="error-message p-4 mb-4 border-round border-1 border-red-200 bg-red-50">
      <p>An error occurred: {{ error.message }}</p>
      <Button 
        label="Refresh" 
        icon="pi pi-refresh" 
        @click="refreshContacts" 
        class="p-button-success mt-2" 
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
            <Column field="name" header="Name" :sortable="true" style="width: 35%">
            </Column>
            <Column field="email" header="Email" :sortable="true" style="width: 35%">
                <template #body="slotProps">
                    <a :href="`mailto:${slotProps.data.email}`" class="text-primary no-underline hover:underline">
                        {{ slotProps.data.email }}
                    </a>
                </template>
            </Column>
            <Column field="phone" header="Phone" :sortable="true" style="width: 35%">
                <template #body="slotProps">
                    <a :href="`tel:${slotProps.data.phone}`" class="text-primary no-underline hover:underline">
                        {{ slotProps.data.phone }}
                    </a>
                </template>
            </Column>
            <Column style="width: 15%" header="Options">
                <template #body>
                    <div class="flex items-center gap-2">
                        <Button icon="pi pi-pencil" type="button" class="p-button-text" @click="editContact(slotProps.data)" v-tooltip="'Edit Contact'"></Button>
                        <Button icon="pi pi-trash" type="button" class="p-button-text" @click="confirmDelete(slotProps.data.id)" v-tooltip="'Delete Contact'"></Button>
                    </div>
                </template>
            </Column>
            <template #empty>
                <div class="text-center p-4">
                <i class="pi pi-users text-6xl text-gray-300 mb-4"></i>
                <p class="text-gray-500">No contacts found.</p>
                </div>
            </template>
        </DataTable>

        <!-- Add/Edit Contact Dialog -->
        <Dialog 
            v-model:visible="displayDialog" 
            :header="isEditMode ? 'Edit Contact' : 'Add New Contact'"
            :modal="true" 
            class="p-fluid" 
            :style="{ width: '450px' }"
        >
        <div class="field mb-4">
            <label for="name" class="font-bold block mb-2">Name *</label>
            <InputText 
                id="name" 
                v-model="contactForm.name" 
                required 
                autofocus 
                :class="{ 'p-invalid': !contactForm.name && displayDialog }"
            />
        </div>
        
        <div class="field mb-4">
            <label for="email" class="font-bold block mb-2">Email *</label>
            <InputText 
                id="email" 
                v-model="contactForm.email" 
                required 
                :class="{ 'p-invalid': !contactForm.email && displayDialog }"
            />
        </div>
        
        <div class="field mb-4">
            <label for="phone" class="font-bold block mb-2">Phone *</label>
            <InputMask 
                id="phone" 
                v-model="contactForm.phone" 
                mask="(99) 99999-9999" 
                :class="{ 'p-invalid': !contactForm.phone && displayDialog }"
            />
        </div>
        
        <div class="field mb-4" v-if="isEditMode">
            <label for="isActive" class="font-bold block mb-2">Status</label>
            <ToggleButton 
                v-model="contactForm.isActive" 
                onLabel="Active" 
                offLabel="Inactive"
                onIcon="pi pi-check" 
                offIcon="pi pi-times"
                class="w-full"
            />
        </div>
        
        <template #footer>
            <Button 
                label="Cancel" 
                icon="pi pi-times" 
                @click="displayDialog = false" 
                class="p-button-text" 
            />
            <Button 
                :label="isEditMode ? 'Update' : 'Save'" 
                icon="pi pi-check" 
                @click="submitForm" 
                :loading="loading"
                :disabled="!contactForm.name"
                autofocus 
            />
        </template>
        </Dialog>

         <!-- Delete Confirmation Dialog -->
        <Dialog 
            v-model:visible="displayDeleteDialog" 
            header="Confirm Delete" 
            :modal="true"
            :style="{ width: '350px' }"
        >
            <div class="confirmation-content">
                <i class="pi pi-exclamation-triangle mr-3" style="font-size: 2rem; color: #f44336;" />
                <span>Are you sure you want to delete this contact?</span>
                <p class="mt-2 text-sm text-gray-600">This action cannot be undone.</p>
            </div>
            <template #footer>
                <Button 
                    label="No" 
                    icon="pi pi-times" 
                    @click="displayDeleteDialog = false" 
                    class="p-button-text" 
                />
                <Button 
                    label="Yes" 
                    icon="pi pi-check" 
                    @click="handleDeleteContact" 
                    class="p-button-danger" 
                    :loading="loading"
                    autofocus 
                />
            </template>
        </Dialog>

    </div>
</template>

<style scoped>
.contact-table {
  min-height: 400px;
}

.error-message {
  background-color: #fef2f2;
  border-color: #fecaca;
}

.loading-message {
  min-height: 400px;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
}

.confirmation-content {
  display: flex;
  align-items: center;
}

:deep(.p-datatable) {
  font-size: 0.875rem;
}

:deep(.p-datatable .p-column-header-content) {
  font-weight: 600;
}

:deep(.p-button.p-button-text) {
  min-width: 2.5rem;
  min-height: 2.5rem;
}
</style>