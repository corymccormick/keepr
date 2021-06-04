<template>
  <div class="modal fade"
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
            {{ keep.name }}
          </h5>
          <button type="button" class="close" data-dismiss="modal" aria-label="Close">
            <span aria-hidden="true">&times;</span>
          </button>
        </div>
        <form @submit.prevent="createCar">
          <div class="modal-body">
            <div class="form-group">
              <label for="make">Make</label>
              <input type="text"
                     class="form-control"
                     id="make"
                     placeholder="Make..."
                     v-model="state.newCar.make"
                     required
              >
            </div>
            <div class="form-group">
              <label for="model">Model</label>
              <input type="text"
                     class="form-control"
                     id="model"
                     placeholder="Model..."
                     v-model="state.newCar.model"
                     required
              >
            </div>
            <div class="form-group">
              <label for="make">Year</label>
              <input type="number"
                     class="form-control"
                     id="year"
                     placeholder="Year..."
                     min="1920"
                     max="2021"
                     v-model="state.newCar.year"
                     required
              >
            </div>
            <div class="form-group">
              <label for="price">Price</label>
              <input type="number"
                     class="form-control"
                     id="price"
                     placeholder="Price..."
                     min="1"
                     v-model="state.newCar.price"
              >
            </div>
            <div class="form-group">
              <label for="description">Description</label>
              <input type="text"
                     class="form-control"
                     id="description"
                     placeholder="Description..."
                     minlength="3"
                     maxlength="20"
                     v-model="state.newCar.description"
              >
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
        </form>
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
