<template>
    <div v-if="vault" class="container">
        <section class="row">
            <div class="text-center">
                <div class="d-flex justify-content-around">
                    <img class="img-fluid rounded cover-img" :src="vault.img" alt="">
                </div>
                <div>
                    <h2>{{ vault.name }}</h2>
                    <p>By {{ vault.creator?.name }}</p>
                    <button @click="destroyVault(vault.id)" class="btn btn-danger">Delete Vault</button>

                </div>
            </div>
        </section>
        <section class="row ">
            <p class="fs-3">Keeps</p>
            <div v-for="keep in keeps" :key="keep.id" class="col-6 mb-3">
                <div>
                    <div class="rounded shadow">
                        <div class=" d-flex justify-content-between rounded align-items-end p-3 img-fluid"
                            :style="{ backgroundImage: `url('${keep.img}')`, backgroundPosition: 'center', backgroundSize: 'cover' }">
                            <p class="glass text-light m-0 p-2">
                                {{ keep.name }}
                            </p>
                            <div class=" fs-2 mdi mdi-delete text-danger" title="Delete Keep from Vault"
                                @click="removeKeep(keep.id)">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</template>


<script>
import { useRoute, useRouter } from 'vue-router';
import { AppState } from '../AppState';
import { computed, reactive, onMounted, watch } from 'vue';
import Pop from '../utils/Pop.js';
import { vaultsService } from '../services/VaultsService.js';
import { logger } from '../utils/Logger.js';
import KeepCard from '../components/KeepCard.vue';
import { keepsService } from '../services/KeepsService.js';

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
            activeKeep: computed(() => AppState.activeKeep),

            async destroyVault(vaultId) {
                try {
                    const vault = AppState.activeKeep
                    const confirmDelete = await Pop.confirm(`Are you sure you want to delete?`)
                    if (!confirmDelete) {
                        return
                    }
                    await vaultsService.destroyVault(vaultId)
                    router.push({ name: 'Home' });
                    Pop.success(`Vault has been deleted`)

                }
                catch (error) {
                    Pop.error(error)
                }

            },

            async removeKeep(keepId) {
                try {

                    const foundKeep = AppState.keeps.find(keep => keep.id == keepId)
                    logger.log(foundKeep)
                    const vaultKeepId = foundKeep.vaultKeepId
                    logger.log('vaultKeep Id', vaultKeepId)
                    await vaultsService.removeVaultKeepById(vaultKeepId)
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