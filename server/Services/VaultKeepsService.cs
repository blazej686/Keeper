



using Microsoft.AspNetCore.Http.HttpResults;

namespace Keeper.Services
{
    public class VaultKeepsService
    {
        private readonly VaultKeepsRepository _vaultKeepsRepository;
        private readonly VaultsService _vaultsService;

        public VaultKeepsService(VaultKeepsRepository vaultKeepsRepository, VaultsService vaultsService)
        {
            _vaultKeepsRepository = vaultKeepsRepository;
            _vaultsService = vaultsService;
        }

        internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepsData, string userId, Vault vaultData)
        {
            VaultKeep vaultKeep = _vaultKeepsRepository.CreateVaultKeep(vaultKeepsData);
            if (vaultData.CreatorId != userId)
            {
                throw new Exception("You can't save to other vaults.");
            }
            if (vaultKeepsData.CreatorId == null)
            {
                throw new Exception("Please sign in to save this keep in your vault.");
            }

            return vaultKeep;
        }

        internal VaultKeep GetVaultKeepById(int vaultKeepId)
        {
            VaultKeep vaultKeep = _vaultKeepsRepository.GetVaultKeepById(vaultKeepId);
            if (vaultKeep == null)
            {
                throw new Exception($"Invalid id {vaultKeepId}");
            }
            return vaultKeep;
        }

        internal string DestroyVaultKeep(int vaultKeepId, string userId)
        {
            VaultKeep vaultKeep = GetVaultKeepById(vaultKeepId);
            if (vaultKeep.CreatorId != userId)
            {
                throw new Exception("Not your VaultKeep to delete");
            }

            _vaultKeepsRepository.DestroyVaultKeep(vaultKeepId);
            return $"{vaultKeepId} has been deleted";
        }

        internal List<Keep> GetKeepsByVaultId(int vaultId, string userId)
        {
            _vaultsService.GetVaultById(vaultId, userId);

            List<Keep> vaultKeeps = _vaultKeepsRepository.GetKeepsByVaultId(vaultId);
            return vaultKeeps;
        }
    }
}
