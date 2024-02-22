<template>
    <div class="container">
        <section class="row justify-content-around">
            <div class="col-6">
                <div class="text-center">
                    <img class="cover-img rounded" :src="activeProfile.coverImg" alt="">
                </div>
                <div class="text-center">
                    <img class="rounded-circle my-3" :src="activeProfile.picture" alt="">
                </div>
                <div class="text-center">
                    <p class="fs-2">
                        {{ activeProfile.name }}
                    </p>
                    <p class="text-center">
                        {{ profileKeeps.length }} Keeps | {{ vaults.length }} Vaults
                    </p>
                </div>
            </div>
        </section>


        <section class="row">

            <h3>Vaults</h3>
            <div v-for="vault in vaults" :key="vault.id" class="mb-3 col-6 col-md-3">
                <div class="rounded shadow">
                    <div>
                        <router-link :to="{ name: 'VaultPage', params: { vaultId: vault.id } }">
                            <div class=" d-flex justify-content-between rounded
                        align-items-end p-3 img-fluid" type="button"
                                :style="{ backgroundImage: `url('${vault.img}')`, backgroundPosition: 'center', backgroundSize: 'cover', minHeight: '25dvh' }">
                                <p class="glass text-light m-0 p-2">
                                    {{ vault.name }}
                                </p>
                            </div>
                        </router-link>
                    </div>
                </div>
                <div>
                </div>
            </div>

        </section>
        <div>
            <section class="row">
                <h3>Keeps</h3>

                <div v-for="keep in keeps" :key="keep.id" class="mb-3 col-6 col-md-3">
                    <div class="rounded shadow">
                        <div @click="setActiveKeep(keep.id)"
                            class=" d-flex justify-content-between rounded align-items-end p-3 img-fluid" type="button"
                            data-bs-toggle="modal" data-bs-target="#staticBackdrop"
                            :style="{ backgroundImage: `url('${keep.img}')`, backgroundPosition: 'center', backgroundSize: 'cover', minHeight: '25dvh' }">
                            <p class="glass text-light m-0 p-2">
                                {{ keep.name }}
                            </p>
                        </div>
                    </div>
                    <div>
                    </div>
                </div>

            </section>
        </div>
    </div>
</template>


<script>
import { useRoute, useRouter } from 'vue-router';
import { AppState } from '../AppState';
import { computed, reactive, onMounted, watch } from 'vue';
import Pop from '../utils/Pop.js';
import { profilesService } from '../services/ProfilesService.js'
import { keepsService } from '../services/KeepsService.js';
import { vaultsService } from '../services/VaultsService.js';
import { logger } from '../utils/Logger.js';

export default {
    setup() {
        const route = useRoute();
        const watchableProfileId = computed(() => route.params.profileId);
        watch(watchableProfileId, () => {
            getProfileById();
            getProfileKeeps();
            getProfileVaults();
        }, { immediate: true });
        async function getProfileById() {
            try {
                const profileId = route.params.profileId;
                await profilesService.getProfileById(profileId);
            }
            catch (error) {
                Pop.error(error);
            }
        }
        ;
        async function getProfileKeeps() {
            try {
                const profileId = route.params.profileId;
                await profilesService.getProfileKeeps(profileId);
            }
            catch (error) {
                Pop.error(error);
            }
        }
        ;
        async function getProfileVaults() {
            try {
                const profileId = route.params.profileId;
                await profilesService.getProfileVaults(profileId);
            }
            catch (error) {
                Pop.error(error);
            }
        }
        return {
            activeProfile: computed(() => AppState.profile),
            profileKeeps: computed(() => AppState.keeps),
            vaults: computed(() => AppState.vaults),
            keeps: computed(() => AppState.keeps),

            setActiveKeep(keepId) {
                try {
                    keepsService.setActiveKeep(keepId);

                }
                catch (error) {
                    Pop.error(error)
                }
            },
        };
    },
};
</script>


<style lang="scss" scoped>
.glass {
    background: rgba(133, 131, 131, 0.45);
    border-radius: 16px;
    box-shadow: 0 4px 30px rgba(0, 0, 0, 0.1);
    backdrop-filter: blur(4.4px);
    -webkit-backdrop-filter: blur(4.4px);
    border: 1px solid rgba(133, 131, 131, 0.3);
}

.cover-img {
    object-fit: cover;
    object-position: center;
    max-height: 40dvh;

}
</style>