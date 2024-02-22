import { AppState } from "../AppState.js"
import { Keep } from "../models/Keep.js"
import { Vault } from "../models/Vault.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class VaultsService {

    async createVault(vaultData) {
        const res = await api.post('api/vaults', vaultData)
        AppState.vaults.push(new Vault(res.data))
    }

    async getVaultById(vaultId) {
        AppState.activeVault = {}
        const res = await api.get(`api/vaults/${vaultId}`)
        const newVault = new Vault(res.data)
        AppState.activeVault = newVault
    }

    async getKeepsByVaultId(vaultId) {
        AppState.keeps = []
        const res = await api.get(`api/vaults/${vaultId}/keeps`)
        logger.log(res.data)

        const newKeeps = res.data.map(pojo => new Keep(pojo))
        logger.log(newKeeps)

        AppState.keeps = newKeeps
    }

    async destroyVault(vaultId) {
        const res = await api.delete(`api/vaults/${vaultId}`)
        AppState.activeVault = {}


    }

    async removeVaultKeepById(vaultKeepId) {

        const res = await api.delete(`api/vaultkeeps/${vaultKeepId}`)
        AppState.keeps = AppState.keeps.filter((keep) => keep.vaultKeepId != vaultKeepId)
    }

}

export const vaultsService = new VaultsService()