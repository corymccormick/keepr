<template>
  <div>
    <div class="modal fade"
         id="new-keep-form"
         tabindex="-1"
         role="dialog"
         aria-labelledby="create a keep"
         aria-hidden="true"
    >
      <div class="modal-dialog" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title" id="exampleModalLabel">
              New Keep
            </h5>
            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
              <span aria-hidden="true">&times;</span>
            </button>
          </div>
          <form @submit.prevent="createKeep">
            <div class="modal-body">
              <div class="form-group">
                <label for="title">Title</label>
                <input type="text"
                       class="form-control"
                       id="title"
                       placeholder="Title...."
                       v-model="state.newVault.name"
                       required
                >
              </div>
              <div class="form-group">
                <label for="decription">Description</label>
                <input type="text"
                       class="form-control"
                       id="description"
                       placeholder="Description..."
                       v-model="state.newVault.description"
                >
              </div>
              <div class="form-group">
                <label for="image">Image</label>
                <input type="text"
                       class="form-control"
                       id="image"
                       placeholder="Image..."
                       v-model="state.newKeep.img"
                       required
                >

                <!-- add is do you want this to be private? -->
              </div>
              >
            </div>
            <div class="form-group">
              <label for="imgUrl">Image Url</label>
              <input type="text" class="form-control" id="imgUrl" placeholder="Url..." v-model="state.newKeep.img">
            </div>
          </form>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-secondary" data-dismiss="modal">
            Close
          </button>
          <button type="submit" class="btn btn-primary">
            Create
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { reactive } from 'vue'
import { vaultsService } from '../services/VaultsService'
import $ from 'jquery'
export default {
  name: 'Component',
  setup() {
    const state = reactive({
      newVault: {}
    })
    return {
      state,
      async createVault() {
        try {
          await vaultsService.createVault(state.newVault)
          // NOTE reseting to the empty object resets the input fields
          state.newVault = {}
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
