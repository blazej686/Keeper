import { AppState } from "../AppState.js"
import { Vault } from "../models/Vault.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class VaultsService {

    async createVault(vaultData) {
        const res = await api.post('api/vaults', vaultData)
        AppState.vaults.push(new Vault(res.data))
    }

    async getVaultById(vaultId) {
        const res = await api.get(`api/vaults/${vaultId}`)
        const newVault = new Vault(res.data)
        AppState.activeVault = newVault
    }

    async getKeepsByVaultId(vaultId) {
        const res = await api.get(`api/vaults/${vaultId}/keeps`)
        const newVaults = res.data.map(pojo => new Vault(pojo))
        AppState.keeps = newVaults
    }

    async destroyVault(vaultId) {
        const res = await api.delete(`api/vaults/${vaultId}`)
        AppState.activeVault = {}


    }

}

export const vaultsService = new VaultsService()