<template>
  <div class="about text-center">
    <div>
      <img class="cover-img rounded" :src="account.coverImg" alt="">
    </div>
    <div>
      <img class="rounded my-3" :src="account.picture" alt="" />
    </div>
    <h1>{{ account.name }}</h1>
    <button class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#editAccountModal">Edit Profile</button>
  </div>

  <div class="container">
    <section class="row justify-content-around">
      <div class="col-6 ">
        <p class=" text-center p-2">
          {{ profileKeeps.length }} Keeps | {{ vaults.length }} Vaults
        </p>
      </div>
    </section>
    <section class="row">
      <h2>Vaults</h2>
      <div v-for="vault in vaults" :key="vault.id" class="mb-3 col-6 col-md-3">
        <div class="rounded shadow">
          <router-link :to="{ name: 'VaultPage', params: { vaultId: vault.id } }">
            <div class=" d-flex justify-content-between rounded align-items-end p-3 img-fluid" type="button"
              :style="{ backgroundImage: `url('${vault.img}')`, backgroundPosition: 'center', backgroundSize: 'cover', minHeight: '25dvh' }">
              <p class="glass text-light m-0 p-2">
                {{ vault.name }}
              </p>
            </div>
          </router-link>
        </div>
        <div>
        </div>
      </div>
    </section>
    <section class="row">

      <h2>Keeps</h2>
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
  <EditAccountModal />
</template>

<script>
import { computed, onMounted, watch } from 'vue';
import { AppState } from '../AppState';
import Pop from '../utils/Pop.js';
import { profilesService } from '../services/ProfilesService.js';
import EditAccountModal from '../components/EditAccountModal.vue';
import { keepsService } from '../services/KeepsService.js';



export default {
  setup() {


    const watchableProfileId = computed(() => AppState.account.id);
    watch(watchableProfileId, () => {
      getProfileKeeps();
      getProfileVaults();
    }, { immediate: true });



    async function getProfileKeeps() {
      try {
        const profileId = AppState.account.id;
        await profilesService.getProfileKeeps(profileId);
      }
      catch (error) {
        Pop.error(error);
      }
    }
    ;
    async function getProfileVaults() {
      try {
        const profileId = AppState.account.id;
        await profilesService.getProfileVaults(profileId);
      }
      catch (error) {
        Pop.error(error);
      }
    }
    return {
      profile: computed(() => AppState.profile),
      account: computed(() => AppState.account),
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
      }
    }
  },
  components: { EditAccountModal }
}
</script>

<style scoped>
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
