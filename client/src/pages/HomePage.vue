<template>
  <div class="container">
    <section class="row">
      <div v-for="keep in keeps" :key="keep.id" class="col-12 col-md-6 mb-3 boarder"></div>
      <HomePageKeepCard :prop="keeps" />
    </section>
  </div>
</template>

<script>
import { onMounted } from 'vue';
import { keepsService } from '../services/KeepsService.js';
import Pop from '../utils/Pop.js';
import { AppState } from '../AppState.js';

export default {
  setup() {
    onMounted(() => {
      getKeeps();

    });

    async function getKeeps() {
      try {
        await keepsService.getKeeps();
      }
      catch (error) {
        Pop.error(error)
      }
    }

    return {
      keeps: computed(() => AppState.keeps)
    }
  },
  components: {}
}
</script>

<style scoped lang="scss"></style>
