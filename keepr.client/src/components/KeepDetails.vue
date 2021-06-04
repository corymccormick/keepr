<template>
  <div class="keep-Details">
    <div class=" row card shadow  ">
      <div class="col-3 m-3 p-1 ">
        <img class="pic " :src="keep.img" alt="" />
        <div class="row">
          {{ keep.name }}
          {{ keep.creator.name }}
          <router-link :to="{name: 'Profile', params:{id: keep.creator.id}}">
            <img class="d-flex justify-content-left rounded-circle m-2 avatar" :src="keep.creator.img" alt="" />
          </router-link>
          <img :src="keep.creator.img" />
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { AppState } from '../AppState'
import { reactive, computed } from 'vue'
import { keepsService } from '../services/KeepsService'
export default {
  name: 'KeepDetails',
  props: {
    keep: {
      type: Object,
      required: true
    }
  },
  setup(props) {
    const state = reactive({
      keeps: computed(() => AppState.keeps)
    })

    return {
      state,
      async deleteKeep() {
        try {
          await keepsService.deleteKeep(props.keep.id)
          Notification.toast('Keep Deleted')
        } catch (error) {
          Notification.toast('Error: ' + error, 'error')
        }
      }

    }
  }
}
</script>

<style lang="scss" scoped>
.pic {
  height: 300px;
  width: 200px;
  background-image: cover;
}
.bg {
background-image: cover;
}
.avatar{
  width: 50px;
  height: 50px;
}

.rounded{
  border-radius: 20%;
}

</style>
