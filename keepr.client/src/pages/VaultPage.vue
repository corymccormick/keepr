<template>
  <div class="vault"></div>
</template>
<script>
import { reactive, computed } from 'vue'
import { AppState } from '../AppState'
// import { keepsService } from './'
export default {
  name: 'Vault',
  setup() {
    const state = reactive({
      keeps: computed(() => AppState.activeKeep)

    })
    // onMounted(async() => {
    //   try {
    //     await keepsService.getKeeps()
    //   } catch (error) {
    //     Notification.toast('Error: ' + error, 'error')
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
