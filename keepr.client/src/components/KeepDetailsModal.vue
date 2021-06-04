<template>
  <div class="modal"
       id="keep-details"
       tabindex="-1"
       role="dialog"
       aria-labelledby="exampleModalLabel"
       aria-hidden="true"
  >
    <div class="modal-dialog" role="document">
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title" id="exampleModalLabel">
            {{ state.activekeep.name }}
          </h5>
          <button type="button" class="close" data-dismiss="modal" aria-label="Close">
            <span aria-hidden="true">&times;</span>
          </button>
        </div>

        <div class="form-group">
          <label for="imgUrl">Image Url</label>
          <input type="text" class="form-control" id="imgUrl" placeholder="Url..." v-model="state.newCar.imgUrl">
        </div>
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-dismiss="modal">
          Close
        </button>
        <button type="submit" class="btn btn-primary">
          delete
        </button>
      </div>
    </div>
  </div>
</template>

<script>
import { reactive } from 'vue'
import $ from 'jquery'
import { keepsService } from '../services/KeepsService'
export default {
  name: 'Component',
  setup() {
    const state = reactive({
      activeKeep: {}
    })
    return {
      state,
      async getKeepById(Id) {
        try {
          await keepsService.getKeepById(Id)
          $('#new-keep-form').modal('hide')
        } catch (error) {
          // eslint-disable-next-line no-console
          console.error(error)
        }
      }
    }
  },
  components: {}
}
</script>

<style lang="scss" scoped>
</style>
