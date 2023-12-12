







namespace Keeper.Repositories;

public class KeepsRepository
{
    private readonly IDbConnection _db;

    public KeepsRepository(IDbConnection db)
    {
        _db = db;
    }

    internal Keep CreateKeep(Keep keepData)
    {
        string sql = @"
INSERT INTO 
keeps(name,description, img,creatorId)
Values(@Name,@Description,@Img,@CreatorId);

SELECT
keeps.*,
acc.*
FROM keeps
JOIN accounts acc ON acc.id = keeps.creatorId
WHERE keeps.id = LAST_INSERT_ID();";

        Keep keep = _db.Query<Keep, Profile, Keep>(sql, KeepBuilder, keepData).FirstOrDefault();
        return keep;
    }

    internal void DestroyKeep(int keepId)
    {
        string sql = @"DELETE FROM keeps WHERE id = @keepId LIMIT 1;";
        _db.Execute(sql, new { keepId });
    }

    internal Keep GetKeepById(int keepId)
    {
        string sql = @"
SELECT
keeps.*,
acc.*
FROM keeps
JOIN accounts acc ON keeps.creatorId = acc.id
WHERE keeps.id = @keepId;";

        // SELECT
        // keeps.*,
        // COUNT(keeps.id) AS kept,
        // acc.*
        // FROM keeps
        // JOIN accounts acc ON keeps.creatorId = acc.id
        // LEFT JOIN vaultKeeps vau ON vau.keepId = keeps.id
        // GROUP BY (keeps.id);";

        Keep keep = _db.Query<Keep, Profile, Keep>(sql, KeepBuilder, new { keepId }).FirstOrDefault();
        return keep;
    }

    internal List<Keep> GetKeeps()
    {
        string sql = @"
SELECT
keeps.*,
acc.*
FROM keeps
JOIN accounts acc ON acc.id = keeps.creatorId;";

        List<Keep> keeps = _db.Query<Keep, Profile, Keep>(sql, KeepBuilder).ToList();
        return keeps;

    }

    internal List<Keep> GetKeepsByProfileId(string profileId, string userId)
    {
        string sql = @"
        SELECT
        k.*,
        acc.*
        FROM keeps k
        JOIN accounts acc ON acc.id = k.creatorId
        WHERE k.creatorId = @profileId;";

        List<Keep> keeps = _db.Query<Keep, Profile, Keep>(sql, KeepBuilder, new { profileId }).ToList();
        return keeps;
    }

    internal Keep UpdateKeep(Keep ogKeep)
    {
        string sql = @"
UPDATE keeps
SET
name = @Name,
description = @Description,
views = @Views
WHERE id = @Id LIMIT 1;

SELECT
keeps.*,
acc.*
FROM keeps
JOIN accounts acc ON acc.id = keeps.creatorId
WHERE keeps.id = @Id;";

        Keep keep = _db.Query<Keep, Profile, Keep>(sql, KeepBuilder, ogKeep).FirstOrDefault();
        return keep;
    }

    private Keep KeepBuilder(Keep keep, Profile profile)
    {
        keep.Creator = profile;
        return keep;
    }
}


