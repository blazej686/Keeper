import { AppState } from "../AppState.js"
import { Keep } from "../models/Keep.js"
import { Profile } from "../models/Profile.js"
import { Vault } from "../models/Vault.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class ProfilesService {
    async getProfileById(profileId) {
        const res = await api.get(`api/profiles/${profileId}`)
        const newProfile = new Profile(res.data)
        AppState.profile = newProfile
    }

    async getProfileKeeps(profileId) {
        const res = await api.get(`api/profiles/${profileId}/keeps`)
        const newKeeps = res.data.map(pojo => new Keep(pojo))
        AppState.keeps = newKeeps
    }

    async getProfileVaults(profileId) {
        const res = await api.get(`api/profiles/${profileId}/vaults`)
        const newVaults = res.data.map(pojo => new Vault(pojo))
        AppState.vaults = newVaults
    }
}
export const profilesService = new ProfilesService()