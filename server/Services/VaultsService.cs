



namespace Keeper.Services
{

    public class VaultsService
    {
        private readonly VaultsRepository _vaultsRepository;

        public VaultsService(VaultsRepository vaultsRepository)
        {
            _vaultsRepository = vaultsRepository;
        }

        internal Vault CreateVault(Vault vaultData)
        {
            Vault vault = _vaultsRepository.CreateVault(vaultData);
            return vault;
        }

        internal string DestroyVault(int vaultId, string userId)
        {
            Vault vault = GetVaultById(vaultId, userId);
            if (vault.CreatorId != userId)
            {
                throw new Exception("Not your vault to delete");
            }
            _vaultsRepository.DestroyVault(vaultId);
            return $"{vault.Name} has been deleted!";
        }

        internal List<Vault> GetMyVaults(string userId)
        {
            List<Vault> myVaults = _vaultsRepository.GetMyVaults(userId);
            return myVaults;
        }

        internal Vault GetVaultById(int vaultId, string userId)
        {
            Vault vault = _vaultsRepository.GetVaultById(vaultId);
            if (vault == null)
            {
                throw new Exception($"invalid ID {vaultId}");
            }
            if (vault.IsPrivate == true && vault.CreatorId != userId)
            {
                throw new Exception("You Broke it...😡");
            }
            return vault;
        }

        internal List<Vault> GetVaultsByProfileId(string profileId, string userId)
        {
            // GetVaultById(,userId);
            List<Vault> vaults = _vaultsRepository.GetVaultsByProfileId(profileId, userId);
            List<Vault> filteredVaults = vaults.FindAll((vault) => vault.IsPrivate == false || vault.CreatorId == userId);
            return filteredVaults;
        }

        internal Vault UpdateVault(int vaultId, string userId, Vault vaultData)
        {
            Vault vaultToBeUpdated = GetVaultById(vaultId, userId);
            if (vaultToBeUpdated.CreatorId != userId)
            {
                throw new Exception("Not your Vault to edit!");
            }

            vaultToBeUpdated.Name = vaultData.Name ?? vaultToBeUpdated.Name;
            vaultToBeUpdated.Description = vaultData.Description ?? vaultToBeUpdated.Description;
            vaultToBeUpdated.Img = vaultData.Img ?? vaultToBeUpdated.Img;
            vaultToBeUpdated.IsPrivate = vaultData.IsPrivate ?? vaultToBeUpdated.IsPrivate;
            Vault vault = _vaultsRepository.UpdateVault(vaultToBeUpdated);

            return vaultToBeUpdated;
        }
    }
}