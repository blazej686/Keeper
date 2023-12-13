<template>
    <div class="rounded shadow">
        <div @click="setActiveKeep(keep.id)" class=" d-flex justify-content-between rounded align-items-end p-3 img-fluid"
            type="button" data-bs-toggle="modal" data-bs-target="#staticBackdrop"
            :style="{ backgroundImage: `url('${keep.img}')`, backgroundPosition: 'center', backgroundSize: 'cover', minHeight: '45dvh' }">
            <p class="glass text-light m-0 p-2">
                {{ keep.name }}
            </p>
            <img class="rounded-circle profile-pic" :src="keep.creator.picture" alt="Creator Profile Picture"
                :title="keep.creator.name">
        </div>
    </div>
    <div>
    </div>
</template>

<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import { Keep } from '../models/Keep.js';
import { logger } from '../utils/Logger.js';
import { keepsService } from '../services/KeepsService.js';
import Pop from '../utils/Pop.js';



export default {
    props: { keep: { type: Keep, required: true } },

    setup() {
        return {

            setActiveKeep(keepId) {
                try {
                    keepsService.setActiveKeep(keepId);

                }
                catch (error) {
                    Pop.error(error)
                }
            }

        }
    }
}
</script>


<style lang="scss" scoped>
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
</style>