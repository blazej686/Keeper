<template>
  <div class="about text-center">
    <h1>Welcome {{ account.name }}</h1>
    <img class="rounded" :src="account.picture" alt="" />
    <p>{{ account.email }}</p>
    <button class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#editAccountModal">Edit Profile</button>
  </div>

  <div class="container">
    <section class="row">
      <div class="col-6 d-flex justify-content-around">
        <div>
          <img class="rounded-circle" :src="activeProfile.picture" alt="">
        </div>
        <div>
          <p class="fs-2">
            {{ activeProfile.name }}
          </p>
          <p>
            {{ profileKeeps.length }} Keeps | {{ vaults.length }} Vaults
          </p>
        </div>
        <section class="row">
          <div>
            <h3>Vaults</h3>
            <div v-for="vault in vaults" :key="vault.id" class="">
              <div class="rounded shadow">
                <div class=" d-flex justify-content-between rounded align-items-end p-3 img-fluid" type="button"
                  :style="{ backgroundImage: `url('${vault.img}')`, backgroundPosition: 'center', backgroundSize: 'cover' }">
                  <p class="glass text-light m-0 p-2">
                    {{ vault.name }}
                  </p>

                </div>
              </div>
              <div>
              </div>
            </div>
          </div>
        </section>
        <div>
          <section class="row">

            <h3>Keeps</h3>
            <div v-for="keep in keeps" :key="keep.id">
              <div class="rounded shadow">
                <div @click="setActiveKeep(keep.id)"
                  class=" d-flex justify-content-between rounded align-items-end p-3 img-fluid" type="button"
                  data-bs-toggle="modal" data-bs-target="#staticBackdrop"
                  :style="{ backgroundImage: `url('${keep.img}')`, backgroundPosition: 'center', backgroundSize: 'cover' }">
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
    </section>
  </div>
  <EditAccountModal />
</template>

<script>
import { computed, onMounted, watch } from 'vue';
import { AppState } from '../AppState';
import Pop from '../utils/Pop.js';
import { profilesService } from '../services/ProfilesService.js';
import EditAccountModal from '../components/EditAccountModal.vue'


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
      account: computed(() => AppState.account),
      activeProfile: computed(() => AppState.profile),
      profileKeeps: computed(() => AppState.keeps),
      vaults: computed(() => AppState.vaults),
      keeps: computed(() => AppState.keeps),
    }
  },
  components: { EditAccountModal }
}
</script>

<style scoped>
img {
  max-width: 100px;
}
</style>
