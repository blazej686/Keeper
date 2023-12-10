<template>
  <div class="container">
    <section class="row">
      <div v-for="keep in keeps" :key="keep.id" class="col-6 col-md-3 mb-3 boarder rounded">
        <KeepCard :keep="keep" />
      </div>
    </section>
  </div>
</template>

<script>
import { computed, onMounted } from 'vue';
import { keepsService } from '../services/KeepsService.js';
import Pop from '../utils/Pop.js';
import { AppState } from '../AppState.js';
import KeepCard from '../components/KeepCard.vue';


export default {
  components: { KeepCard },
  setup() {
    async function getKeeps() {
      try {
        await keepsService.getKeeps();
      }
      catch (error) {
        Pop.error(error)
      }
    }
    onMounted(() => {
      getKeeps();

    });


    return {
      keeps: computed(() => AppState.keeps)
    }
  }
}
</script>

<style scoped lang="scss"></style>
