<template>
    <div class="modal fade" id="editAccountModal" tabindex="-1" aria-labelledby="editAccountModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5" id="editAccountModalLabel">Edit Account</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <form @submit.prevent="editAccount">
                        <div class="mb-3">
                            <label for="accountName" class="form-label">Name</label>
                            <input v-model="editable.name" type="text" class="form-control" id="accountName"
                                aria-describedby="name" maxlength="255">
                            <label for="profilePicture" class="form-label">Profile Picture</label>
                            <input v-model="editable.Picture" type="url" class="form-control" id="profilePicture"
                                aria-describedby="profilePicture" maxlength="500">
                            <label for="coverImg" class="form-label">Cover Image</label>
                            <input v-model="editable.coverImg" type="url" class="form-control" id="coverImg"
                                aria-describedby="coverImg" maxlength="500">
                        </div>
                        <button type="submit" class="btn btn-primary" data-bs-dismiss="modal">Submit</button>
                    </form>
                </div>

            </div>
        </div>
    </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted, ref } from 'vue';
import { accountService } from '../services/AccountService.js';
import Pop from '../utils/Pop.js';

export default {
    setup() {
        const editable = ref({})
        return {
            editable,

            async editAccount() {
                try {
                    const accountData = editable.value;
                    await accountService.editAccount(accountData);
                    editable.value = {};
                    Pop.success('account edited!')
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

