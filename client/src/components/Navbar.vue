<template>
  <nav class="navbar navbar-expand-sm navbar-dark px-3 ">
    <div>
      <div class="btn-group ">
        <router-link class="navbar-brand d-flex" :to="{ name: 'Home' }">
          <div class="d-flex flex-column align-items-center">
            <img alt="logo" src="../assets/img/keeper-logo.png" height="45" />
          </div>
        </router-link>
        <div class=" d-flex align-items-center">
          <button v-if="account.id && route.name == 'Home'"
            class="btn btn-secondary text-light lighten-30 dropdown-toggle" type="button" id="triggerId"
            data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
            Create<i class="mdi mdi-dropdown"></i>
          </button>
          <div class="dropdown-menu dropdown-menu-start" aria-labelledby="triggerId">
            <button class="dropdown-item" data-bs-toggle="modal" data-bs-target="#createKeepModal">Create Keep</button>
            <button class="dropdown-item" data-bs-toggle="modal" data-bs-target="#createVaultModal">Create Vault</button>
          </div>
        </div>
      </div>
    </div>
    <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarText"
      aria-controls="navbarText" aria-expanded="false" aria-label="Toggle navigation">
      <span class="navbar-toggler-icon"></span>
    </button>
    <div class="collapse navbar-collapse" id="navbarText">
      <ul class="navbar-nav me-auto">

      </ul>
      <!-- LOGIN COMPONENT HERE -->

      <Login />
    </div>
  </nav>
</template>

<script>
import { computed, onMounted, ref } from 'vue';
import { loadState, saveState } from '../utils/Store.js';
import Login from './Login.vue';
import { AppState } from '../AppState.js';
import { useRoute } from 'vue-router';
export default {
  setup() {
    const route = useRoute();
    const theme = ref(loadState('theme') || 'light')

    onMounted(() => {
      document.documentElement.setAttribute('data-bs-theme', theme.value)
    })

    return {
      route,
      theme,
      account: computed(() => AppState.account),
      toggleTheme() {
        theme.value = theme.value == 'light' ? 'dark' : 'light'
        document.documentElement.setAttribute('data-bs-theme', theme.value)
        saveState('theme', theme.value)
      }
    }
  },
  components: { Login }
}
</script>

<style scoped>
a:hover {
  text-decoration: none;
}

.nav-link {
  text-transform: uppercase;
}

.navbar-nav .router-link-exact-active {
  border-bottom: 2px solid var(--bs-success);
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}

@media screen and (min-width: 768px) {
  nav {
    height: 64px;
  }
}
</style>
