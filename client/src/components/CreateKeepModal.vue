<template>
    <div class="modal fade " id="createKeepModal" tabindex="-1" aria-labelledby="createKeepModalLabel" aria-hidden="true">
        <div class="modal-dialog ">
            <div class="modal-content bg-dark">
                <div class="modal-header">
                    <h1 class="modal-title fs-5 " id="createKeepModalLabel">Create Keep</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body ">
                    <form @submit.prevent="createKeep">
                        <div class="mb-3">
                            <label for="name" class="form-label">Name</label>
                            <input v-model="editable.name" type="text" class="form-control" id="name" required
                                maxlength="255">
                            <label for="description" class="form-label">Description</label>
                            <input v-model="editable.description" type="text" class="form-control" id="description" required
                                maxlength="500">
                            <label for="Img" class="form-label">Img</label>
                            <input v-model="editable.Img" type="url" class="form-control" id="Img" required
                                maxlength="1000">
                        </div>
                        <button type="submit" class="btn btn-primary">Submit</button>
                    </form>
                </div>

            </div>
        </div>
    </div>
</template>


<script>
import { Modal } from 'bootstrap';
import { computed, reactive, onMounted, ref } from 'vue';
import { keepsService } from '../services/KeepsService.js'
export default {
    setup() {
        const editable = ref({})

        return {
            editable,
            async createKeep() {
                try {
                    const keepData = editable.value
                    await keepsService.createKeep(keepData)
                    editable.value = {}
                    Modal.getOrCreateInstance('#createKeepModal').hide()

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

