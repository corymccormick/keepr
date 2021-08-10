<template>
  <div class="container-fluid">
    <div class="row">
      <div class="col mt-3 ">
        <img class="" :src="profile.picture" alt="">
      </div>
      <div class="col mt-3">
        {{ profile.name }} <br /> Vaults: {{ 0 }} <br /> Keeps: {{ 0 }}
      </div>
    </div>
  </div>
  <!-- create keep modal -->
  <!-- create vault modal -->
</template>

<script>
import { reactive, computed, watchEffect } from 'vue'
import { useRoute } from 'vue-router'
import { AppState } from '../AppState'
import { profilesService } from '../services/ProfilesService'

export default {
  name: 'Profile',
  setup() {
    const route = useRoute()
    watchEffect(() => {
      profilesService.getProfile(route.params.id)
      profilesService.getProfileKeeps(route.params.id)
      profilesService.getProfileVaults(route.params.id)
      //  await?
    })

    return reactive({
      profile: computed(() => AppState.profile),
      keeps: computed(() => AppState.keeps),
      vaults: computed(() => AppState.vaults),
      acccount: computed(() => AppState.account)
    })
  }
}

</script>

<style>

</style>
