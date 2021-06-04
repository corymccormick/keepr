<template>
  <span>This is a profile page</span>
</template>

<script>
import { reactive, computed } from 'vue'
import { AppState } from '../AppState'
// import { profilesService } from '../services/ProfilesService'

export default {
  name: 'Profile',
  setup() {
    const state = reactive({
      profile: computed(() => AppState.activeProfile),
      keeps: computed(() => AppState.keeps),
      vaults: computed(() => AppState.vaults),
      acccount: computed(() => AppState.account)
    })
    // onMounted(async() => {
    //   try {
    //     await profilesService.getProfile(route.params.id)
    //     await profilesService.getProfileKeeps(route.params.id)
    //     await profilesService.getProfileVaults(route.params.id)
    //   } catch (error) {
    //   }
    // })
    return {
      state
    }
  }
}
</script>

<style>

</style>
