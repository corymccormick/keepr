import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class ProfilesService {
  async getProfile(id) {
    try {
      const res = await api.get('/api/profiles/' + id)
      AppState.profile = res.data
    } catch (err) {
      logger.error('Something went wrong', err)
    }
  }

  async getProfileKeeps(id) {
    const res = await api.get(`api/profiles/${id}/keeps`)
    AppState.keeps = res.data
  }

  async getProfileVaults(id) {
    const res = await api.get(`api/profiles/${id}/vaults`)
    AppState.keeps = res.data
  }
}

export const profilesService = new ProfilesService()
