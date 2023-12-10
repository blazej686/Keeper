





namespace Keeper.Repositories
{
    public class VaultsRepository
    {
        IDbConnection _dbConnection;

        public VaultsRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        internal Vault CreateVault(Vault vaultData)
        {
            string sql = @"
            INSERT INTO vaults
            (name,description, img,creatorId, isPrivate)
            Values(@Name,@Description,@Img,@CreatorId, @IsPrivate);

            SELECT
            vaults.*,
            acc.*
            FROM vaults
            JOIN accounts acc ON acc.id = vaults.creatorId
            WHERE vaults.id = LAST_INSERT_ID();";

            Vault vault = _dbConnection.Query<Vault, Profile, Vault>(sql, VaultBuilder, vaultData).FirstOrDefault();
            return vault;
        }


        internal void DestroyVault(int vaultId)
        {
            string sql = @"
            DELETE FROM vaults WHERE id = @vaultId LIMIT 1;";
            _dbConnection.Execute(sql, new { vaultId });
        }

        internal List<Vault> GetMyVaults(string userId)
        {
            string sql = @"
            SELECT
            vau.*,
            acc.*
            FROM vaults vau
            JOIN accounts acc ON acc.id = vau.creatorId
            WHERE vau.creatorId = @userId;";
            List<Vault> vaults = _dbConnection.Query<Vault, Profile, Vault>(sql, VaultBuilder, new { userId }).ToList();
            return vaults;
        }

        internal Vault GetVaultById(int vaultId)
        {
            string sql = @"
            SELECT
            vaults.*,
            acc.*
            FROM vaults
            JOIN accounts acc ON acc.id = vaults.creatorId
            WHERE vaults.id = @vaultId;";

            Vault vault = _dbConnection.Query<Vault, Profile, Vault>(sql, VaultBuilder, new { vaultId }).FirstOrDefault();
            return vault;
        }

        internal Vault UpdateVault(Vault vaultToBeUpdated)
        {
            string sql = @"
            UPDATE vaults
            SET
            name = @Name,
            description = @Description,
            isPrivate = @IsPrivate,
            img = @Img
            WHERE id = @Id LIMIT 1;

            SELECT
            vau.*,
            acc.*
            FROM vaults vau
            JOIN accounts acc ON acc.id = vau.creatorId
            WHERE vau.id = @Id;";

            Vault vault = _dbConnection.Query<Vault, Profile, Vault>(sql, VaultBuilder, vaultToBeUpdated).FirstOrDefault();
            return vault;
        }

        internal List<Vault> GetVaultsByProfileId(string profileId, string userId)
        {
            string sql = @"
        SELECT
        v.*,
        acc.*
        FROM vaults v
        JOIN accounts acc ON acc.id = v.creatorId
        WHERE v.creatorId = @profileId;";

            List<Vault> vaults = _dbConnection.Query<Vault, Profile, Vault>(sql, VaultBuilder, new { profileId }).ToList();
            return vaults;
        }

        private Vault VaultBuilder(Vault vault, Profile profile)
        {
            vault.Creator = profile;
            return vault;
        }
    }


}