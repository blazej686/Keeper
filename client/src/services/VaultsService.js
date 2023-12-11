import { api } from "./AxiosService.js"

class VaultsService {

    async getVaults(userId) {

        const res = await api.get('api/')

    }
}

export const vaultsService = new VaultsService()