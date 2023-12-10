<template>
    <div class="rounded shadow">
        <img :src="keep.img" alt="Keep img">
    </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import { Keep } from '../models/Keep.js';
export default {
    props: {
        keeps: { type: Keep, required: true }
    },

    setup() {
        return {}
    }
}
</script>


<style lang="scss" scoped></style>