<template>
    <div class="modal fade" id="createVaultModal" tabindex="-1" aria-labelledby="editKeepModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content bg-dark">
                <div class="modal-header">
                    <h1 class="modal-title fs-5" id="createVaultModal">Create Vault</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <form @submit.prevent="createVault">
                        <div class="mb-3">
                            <label for="createName" class="form-label">Name</label>
                            <input v-model="editable.name" type="text" class="form-control" id="createName" required
                                maxlength="255">
                            <label for="createDescription" class="form-label">Description</label>
                            <input v-model="editable.description" type="text" class="form-control" id="createDescription"
                                required maxlength="500">
                            <label for="createImg" class="form-label">Img</label>
                            <input v-model="editable.Img" type="url" class="form-control" id="createImg" required
                                maxlength="1000">
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
import Pop from '../utils/Pop.js';
export default {
    setup() {
        const editable = ref({})


        return {
            editable,
            async createVault() {
                try {
                    const vaultData = editable.value
                    await vaultsService.createVault(vaultData)
                    editable.value = {}
                    Modal.getOrCreateInstance('#createVaultModal').hide()

                }
                catch (error) {
                    Pop.error(error)
                }
            }
        }
    }
};
</script>


<style lang="scss" scoped></style>