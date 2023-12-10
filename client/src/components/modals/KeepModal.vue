<template>
    <div class="modal fade" id="staticBackdrop" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1"
        aria-labelledby="staticBackdropLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">

                <div class="modal-body">
                    <div class="container-fluid">
                        <div class="text-end">
                            <button type="button" class="btn-close text-end" data-bs-dismiss="modal"
                                aria-label="Close"></button>
                        </div>
                        <section class="row">
                            <div class="col-6">
                                <img class="img-fluid" :src="activeKeep.img" alt="Keep Img">
                            </div>
                            <div class="col-6">
                                <div>
                                    <p>views 0</p>
                                    <p>kept 0</p>
                                </div>
                                <div>
                                    <h4>{{ activeKeep.name }}</h4>
                                    <p>{{ activeKeep.description }}</p>
                                </div>

                            </div>
                        </section>
                    </div>
                    <form @submit.prevent="">
                        <div class="dropdown">
                            <button class="btn btn-secondary dropdown-toggle" type="button" data-bs-toggle="dropdown"
                                aria-expanded="false">
                                Vaults
                            </button>
                            <ul class="dropdown-menu">
                                <li><button @click="" class="dropdown-item">Action</button></li>
                                <li><button class="dropdown-item">Another action</button></li>
                                <li><button class="dropdown-item">Something else here</button></li>
                            </ul>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>
</template>


<script>

import { computed, reactive, onMounted } from 'vue';
import { AppState } from '../../AppState.js';


export default {
    setup() {
        return {
            activeKeep: computed(() => AppState.activeKeep)
        }
    }
};
</script>


<style lang="scss" scoped></style>