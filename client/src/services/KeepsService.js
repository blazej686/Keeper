import { AppState } from "../AppState.js"
import { Keep } from "../models/Keep.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class KeepsService {

    async getKeeps() {
        const res = await api.get('api/keeps')
        const newKeeps = res.data.map((keep) => new Keep(keep))
        AppState.keeps = newKeeps
    }

    async setActiveKeep(keepId) {
        AppState.activeKeep = {}
        const res = await api.get(`api/keeps/${keepId}`)
        const keepData = new Keep(res.data)
        AppState.activeKeep = keepData
    }
    async createKeep(keepData) {
        const res = await api.post('api/keeps', keepData)
        AppState.keeps.push(new Keep(res.data))
    }

    async destroyKeep(keepId) {
        const res = await api.delete(`api/keeps/${keepId}`)
        AppState.keeps = AppState.keeps.filter((keep) => keep.Id != keepId)
        AppState.activeKeep = null
    }


}
export const keepsService = new KeepsService()