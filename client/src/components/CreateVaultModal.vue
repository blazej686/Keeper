<template>
    <div class="modal fade" id="createVaultModal" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1"
        aria-labelledby="editKeepModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5" id="createVaultModal">Create Vault</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <form @submit.prevent="createVault">
                        <div class="mb-3">
                            <label for="name" class="form-label">Name</label>
                            <input v-model="editable.name" type="text" class="form-control" id="name" required>
                            <label for="description" class="form-label">Description</label>
                            <input v-model="editable.description" type="text" class="form-control" id="description"
                                required>
                            <label for="Img" class="form-label">Img</label>
                            <input v-model="editable.Img" type="url" class="form-control" id="Img" required>
                        </div>
                        <div class="mb-3 form-check">
                            <input v-model="editable.isPrivate" type="checkbox" class="form-check-input" id="isPrivate">
                            <label class="form-check-label" for="isPrivate">Do you want this to be Private?</label>
                        </div>
                        <button type="submit" class="btn btn-primary">Submit</button>
                    </form>
                </div>
            </div>
        </div>
    </div>
</template>


<script>
import { computed, reactive, onMounted, ref } from 'vue';
import { vaultsService } from '../services/VaultsService.js';
import { Modal } from 'bootstrap';
export default {
    setup() {
        const editable = ref({})


        return {
            editable,
            async createVault() {
                const vaultData = editable.value
                await vaultsService.createVault(vaultData)
                editable.value = {}
                Modal.getOrCreateInstance('#createVaultModal').hide()
            }
        }
    }
};
</script>


<style lang="scss" scoped></style>