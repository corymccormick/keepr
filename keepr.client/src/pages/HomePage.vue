<template>
  <div class="container-fluid d-flex">
    <div class="row justify-content-around ">
      <KeepDetails
        v-for="keep in state.keeps"
        :key="keep.id"
        :keep="keep"
      />
    </div>
  </div>
</template>

<script>
import { onMounted, reactive, computed } from 'vue'
import { AppState } from '../AppState'
import { keepsService } from '../services/KeepsService'

export default {
  name: 'Home',
  setup() {
    const state = reactive({
      keeps: computed(() => AppState.keeps)
    })
    onMounted(async() => {
      try {
        await keepsService.getKeeps()
      } catch (error) {
        // eslint-disable-next-line no-console
        console.error('Can Not get keeps')
      }
    })
    return {
      state
    }
  }
}
</script>

<style scoped lang="scss">
.home{
  text-align: center;
  user-select: none;
  > img{
    height: 200px;
    width: 200px;
  }
}
</style>
