


namespace Keeper.Repositories
{
    public class ProfilesRepository
    {

        private readonly IDbConnection _dbConnection;

        public ProfilesRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        internal Profile GetProfileById(string profileId)
        {
            string sql = @"
SELECT 
acc.*
FROM accounts acc
WHERE acc.id = @profileId;";

            Profile profile = _dbConnection.Query<Profile>(sql, new { profileId }).FirstOrDefault();
            return profile;
        }
    }
}