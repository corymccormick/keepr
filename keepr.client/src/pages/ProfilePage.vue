<template>
  <span>This is a profile page</span>
  <!-- create keep modal -->
  <!-- create vault modal -->
  {{ profile }}
</template>

<script>
import { reactive, computed, watchEffect } from 'vue'
import { useRoute } from 'vue-router'
import { AppState } from '../AppState'
import { profilesService } from '../services/ProfilesService'

export default {
  setup() {
    const route = useRoute()
    watchEffect(() => {
      profilesService.getProfile(route.params.id)
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

      // name: 'Profile',
      // props: {
      //   profileProp: {
      //     type: Object,
      //     required: true
      //   }
      // }

        // })
        // onMounted(async() => {
        //   try {
        //     await profilesService.getProfile(route.params.id)
        //     await profilesService.getProfileKeeps(route.params.id)
        //     await profilesService.getProfileVaults(route.params.id)
        //   } catch (error) {
        //   }
        // })
