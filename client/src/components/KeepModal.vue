<template>
    <div class="modal fade" id="staticBackdrop" tabindex="-1" aria-labelledby="staticBackdropLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">

                <div class="modal-body">
                    <div class="container-fluid">
                        <div class="text-end">
                            <button type="button" class="btn-close text-end" data-bs-dismiss="modal"
                                aria-label="Close"></button>
                        </div>
                        <section class="row">
                            <div class="col-6">
                                <img class="img-fluid" :src="activeKeep.img" alt="Keep Img" :title="activeKeep.name">
                            </div>
                            <div v-if="activeKeep.id" class="col-6">
                                <div>
                                    <p>Views {{ activeKeep.views }}</p>
                                    <p>Kept {{ activeKeep.kept }}</p>
                                </div>
                                <div>
                                    <h4>{{ activeKeep.name }}</h4>
                                    <p>{{ activeKeep.description }}</p>
                                    <div class="text-end">
                                        <router-link
                                            :to="{ name: 'ProfilePage', params: { profileId: activeKeep.creator.id } }">
                                            <img class="rounded-circle profile-pic " data-bs-dismiss="modal"
                                                :title="activeKeep.creator.name" :src="activeKeep.creator.picture" alt="">
                                        </router-link>
                                    </div>
                                </div>
                            </div>
                        </section>
                    </div>
                    <form v-if="account.id" @submit.prevent="">
                        <div class="dropdown">
                            <button @click="getProfileVaults()" class="btn btn-secondary dropdown-toggle mt-3" type="button"
                                data-bs-toggle="dropdown" aria-expanded="false">
                                Add to Vault
                            </button>
                            <ul class="dropdown-menu">
                                <div>
                                    <li v-for="vault in vaults" :key="vault.id">
                                        <button @click="addKeepToVault(vault.id)" class="dropdown-item">{{ vault.name
                                        }}</button>
                                    </li>
                                </div>
                            </ul>
                        </div>
                    </form>
                    <div v-if="account.id" class="text-end">
                        <button v-if="account.id == activeKeep.creatorId" @click="destroyKeep(activeKeep.id)"
                            class="btn btn-danger">Delete Keep</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>


<script>

import { computed, reactive, onMounted } from 'vue';
import { AppState } from '../AppState.js';
import Pop from '../utils/Pop.js';
import { useRouter } from 'vue-router';
import { Modal } from 'bootstrap';
import { keepsService } from '../services/KeepsService.js'
import { profilesService } from '../services/ProfilesService.js';
import { logger } from '../utils/Logger.js';


export default {
    setup() {

        return {
            activeKeep: computed(() => AppState.activeKeep),
            vaults: computed(() => AppState.vaults),
            account: computed(() => AppState.account),

            async destroyKeep(keepId) {
                try {
                    const keep = AppState.activeKeep
                    const confirmDelete = await Pop.confirm(`Are you sure you want to delete ${keep.name}?`)
                    if (!confirmDelete) {
                        return
                    }
                    await keepsService.destroyKeep(keepId)
                    Modal.getOrCreateInstance('#staticBackdrop').hide()
                    Pop.success(`${keep.name} has been deleted`)

                }
                catch (error) {
                    Pop.error(error)
                }
            },

            async getProfileVaults() {
                try {
                    const profileId = AppState.account.id;
                    await profilesService.getProfileVaults(profileId);
                }
                catch (error) {
                    Pop.error(error);
                }
            },

            async addKeepToVault(vaultId) {
                try {
                    const keepId = this.activeKeep.id
                    await keepsService.addKeepToVault(keepId, vaultId)
                    AppState.activeKeep.kept++
                    Pop.success('Keep has been added to the Vault!')


                }
                catch (error) {
                    Pop.error(error)
                }
            }

        }
    }
};
</script>


<style lang="scss" scoped>
.profile-pic {
    object-fit: cover;
    object-position: center;
    height: 5vh;
}
</style>