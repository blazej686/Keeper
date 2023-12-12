<template>
    <div class="modal fade" id="staticBackdrop" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1"
        aria-labelledby="staticBackdropLabel" aria-hidden="true">
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
                                <img class="img-fluid" :src="activeKeep.img" alt="Keep Img">
                            </div>
                            <div v-if="activeKeep.id" class="col-6">
                                <div>
                                    <p>Views {{ activeKeep.views }}</p>
                                    <p>kept 0</p>
                                </div>
                                <div>
                                    <h4>{{ activeKeep.name }}</h4>
                                    <p>{{ activeKeep.description }}</p>
                                    <div class="text-end">
                                        <router-link
                                            :to="{ name: 'ProfilePage', params: { profileId: activeKeep.creator.id } }">
                                            <img class="rounded-circle profile-pic" :src="activeKeep.creator.picture"
                                                alt="">
                                        </router-link>
                                    </div>
                                </div>
                            </div>
                        </section>
                    </div>
                    <form @submit.prevent="">
                        <div class="dropdown">
                            <button class="btn btn-secondary dropdown-toggle" type="button" data-bs-toggle="dropdown"
                                aria-expanded="false">
                                Add to Vault
                            </button>
                            <ul class="dropdown-menu">
                                <li v-for="vault in vaults" :key="vault.id"><button @click="" class="dropdown-item">{{
                                    vault.name }}</button></li>
                            </ul>
                        </div>
                    </form>
                    <div class="text-end">
                        <button @click="destroyKeep(activeKeep.id)" class="btn btn-danger">Delete Keep</button>
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


export default {
    setup() {
        const router = useRouter();

        return {
            activeKeep: computed(() => AppState.activeKeep),
            vaults: computed(() => AppState.userVaults),

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