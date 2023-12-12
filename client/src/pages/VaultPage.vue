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
                <KeepCard :keep="keep" />
            </div>
        </section>
    </div>
</template>


<script>
import { useRoute } from 'vue-router';
import { AppState } from '../AppState';
import { computed, reactive, onMounted, watch } from 'vue';
import Pop from '../utils/Pop.js';
import { vaultsService } from '../services/VaultsService.js';
import { logger } from '../utils/Logger.js';
import KeepCard from '../components/KeepCard.vue';
import { router } from '../router.js';

export default {
    setup() {
        const route = useRoute();
        const watchableVaultId = computed(() => route.params.vaultId);
        watch(watchableVaultId, () => {
            getVaultById();
            getKeepsByVaultId();
        }, { immediate: true });
        async function getVaultById() {
            try {
                const vaultId = route.params.vaultId;
                vaultsService.getVaultById(vaultId);
            }
            catch (error) {
                Pop.error(error);
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
</style>