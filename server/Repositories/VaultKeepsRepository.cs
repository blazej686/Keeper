



namespace Keeper.Repositories
{
    public class VaultKeepsRepository
    {

        private readonly IDbConnection _dbConnection;

        public VaultKeepsRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepsData)
        {
            string sql = @"
            INSERT INTO 
            vaultKeeps (id,vaultId, keepId,creatorId)
            VALUES(@Id, @VaultId, @KeepId,@CreatorId);

            SELECT * FROM vaultKeeps WHERE id = LAST_INSERT_ID();";

            VaultKeep vaultKeep = _dbConnection.Query<VaultKeep>(sql, vaultKeepsData).FirstOrDefault();
            return vaultKeep;

        }


        internal List<Keep> GetKeepsByVaultId(int vaultId)
        {
            string sql = @"
            SELECT
            vau.*,
            keeps.*,
            acc.*
            FROM vaultKeeps vau
            JOIN keeps ON vau.keepId = keeps.id
            JOIN accounts acc ON acc.id = vau.creatorId
            WHERE vau.vaultId = @vaultId;";

            List<Keep> vaultKeeps = _dbConnection.Query<VaultKeep, Keep, Profile, Keep>(sql, VaultKeepBuilder, new { vaultId }).ToList();
            return vaultKeeps;
        }
        internal void DestroyVaultKeep(int vaultKeepId)
        {
            string sql = @"DELETE FROM vaultKeeps WHERE id = @vaultKeepId;";
            _dbConnection.Execute(sql, new { vaultKeepId });
        }

        internal VaultKeep GetVaultKeepById(int vaultKeepId)
        {
            string sql = @"
            SELECT
            vau.*,
            acc.*
            FROM vaultKeeps vau
            JOIN accounts acc ON acc.id = vau.CreatorId
            WHERE vau.id = @vaultKeepId;";



            VaultKeep vaultKeep = _dbConnection.Query<VaultKeep, Profile, VaultKeep>(sql, (vaultKeep, profile) =>
            {
                vaultKeep.Creator = profile;
                return vaultKeep;
            }, new { vaultKeepId }).FirstOrDefault();
            return vaultKeep;
        }

        private Keep VaultKeepBuilder(VaultKeep vaultKeep, Keep keep, Profile profile)
        {
            keep.Creator = profile;

            keep.VaultKeepId = vaultKeep.Id;

            return keep;
        }


    }

}
