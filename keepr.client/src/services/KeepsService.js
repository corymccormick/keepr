import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'
import router from '../router'
class KeepsService {
  async getKeeps() {
    try {
      const res = await api.get('api/keeps')
      AppState.keeps = res.data
    } catch (error) {
      logger.error(error)
    }
  }

  async getKeepById(id) {
    try {
      const res = await api.get('api/keeps/' + id)
      AppState.keeps = res.data
    } catch (error) {
      logger.error(error)
    }
  }

  async createKeep(data) {
    const res = await api.post('api/keep', data)
    const res2 = await api.get(`api/profiles/${res.data.creatorId}/posts`)
    AppState.posts = res2.data
  }

  async deleteKeep(id) {
    await api.delete('api/keeps/' + id)
    AppState.keeps = AppState.keeps.keeps.filter(keep => keep.id !== id)
    router.push({ name: 'Home' })
  }
}
export const keepsService = new KeepsService()
