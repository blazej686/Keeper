<template>
  <header>
    <Navbar />
  </header>
  <main>
    <router-view />
  </main>

  <KeepModal />
  <CreateKeepModal />
  <CreateVaultModal />
</template>

<script>
import { computed } from 'vue';
import { AppState } from './AppState';
import Navbar from './components/Navbar.vue';
import KeepModal from './components/KeepModal.vue';
import CreateKeepModal from './components/CreateKeepModal.vue';
import CreateVaultModal from './components/CreateVaultModal.vue';


export default {
  setup() {
    return {
      appState: computed(() => AppState)
    }
  },
  components: { Navbar, KeepModal, CreateKeepModal, CreateVaultModal }
}
</script>
<style lang="scss">
@import "./assets/scss/main.scss";
</style>
