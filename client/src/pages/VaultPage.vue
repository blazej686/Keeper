<template>
    <div v-if="vault" class="container">
        <section class="row">
            <div>
                <div class="col-4"
                    :style="{ backgroundImage: `url('${vault.img}')`, backgroundPosition: 'center', backgroundSize: 'cover' }">
                    <h2>{{ vault.name }}</h2>
                    <p>By {{ vault.creator?.name }}</p>
                    <button @click="destroyVault(vault.id)" class="btn btn-danger">Delete Vault</button>
                </div>
            </div>
        </section>
        <section class="row">
            <div v-for="keep in keeps" :key="keep.id" class="col-6">
                <div>
                    <div class="rounded shadow">
                        <div @click="setActiveKeep(keep.id)"
                            class=" d-flex justify-content-between rounded align-items-end p-3 img-fluid mdi mdi-delete"
                            :style="{ backgroundImage: `url('${keep.img}')`, backgroundPosition: 'center', backgroundSize: 'cover' }">
                            <p class="glass text-light m-0 p-2">
                                {{ keep.name }}
                            </p>
                            <div class=" fs-2 mdi mdi-delete">
                                <!-- <img class="rounded-circle profile-pic" :src="keep.creator.picture"
                                    alt="Creator Profile Picture" :title="keep.creator.name"> -->
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</template>
<!-- type="button" data-bs-toggle="modal" data-bs-target="#staticBackdrop" -->


<script>
import { useRoute, useRouter } from 'vue-router';
import { AppState } from '../AppState';
import { computed, reactive, onMounted, watch } from 'vue';
import Pop from '../utils/Pop.js';
import { vaultsService } from '../services/VaultsService.js';
import { logger } from '../utils/Logger.js';
import KeepCard from '../components/KeepCard.vue';

export default {
    setup() {
        const router = useRouter();
        const route = useRoute();
        const watchableVaultId = computed(() => route.params.vaultId);
        watch(watchableVaultId, () => {
            getVaultById();
            getKeepsByVaultId();
        }, { immediate: true });

        async function getVaultById() {
            try {
                const vaultId = route.params.vaultId;
                await vaultsService.getVaultById(vaultId);
            }
            catch (error) {
                Pop.error(error)
                if (error.response.data.includes('😡')) {
                    console.log('do i make it here');
                    router.push({ name: 'Home' })
                }
            }
        }
        ;
        async function getKeepsByVaultId() {
            const vaultId = route.params.vaultId;
            vaultsService.getKeepsByVaultId(vaultId);
        }
        return {
            vault: computed(() => AppState.activeVault),
            keeps: computed(() => AppState.keeps),

            async destroyVault(vaultId) {
                try {
                    const vault = AppState.activeKeep
                    const confirmDelete = await Pop.confirm(`Are you sure you want to delete ${vault.name}?`)
                    if (!confirmDelete) {
                        return
                    }
                    await vaultsService.destroyVault(vaultId)
                    router.push({ name: 'Home' });
                    Pop.success(`${vault.name} has been deleted`)

                }
                catch (error) {
                    Pop.error(error)
                }

            }
        };
    },
    components: { KeepCard }
};
</script>


<style lang="scss" scoped>
.hight {
    height: 30%;

}

.profile-pic {
    object-fit: cover;
    object-position: center;
    height: 5vh;
}

.glass {
    background: rgba(133, 131, 131, 0.28);
    border-radius: 16px;
    box-shadow: 0 4px 30px rgba(0, 0, 0, 0.1);
    backdrop-filter: blur(4.4px);
    -webkit-backdrop-filter: blur(4.4px);
    border: 1px solid rgba(133, 131, 131, 0.3);
}
</style>