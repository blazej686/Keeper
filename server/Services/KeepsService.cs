



namespace Keeper.Services
{
    public class KeepsService
    {

        private readonly KeepsRepository _keepsRepository;

        public KeepsService(KeepsRepository keepsRepository)
        {
            _keepsRepository = keepsRepository;
        }

        internal Keep CreateKeep(Keep keepData)
        {
            Keep keep = _keepsRepository.CreateKeep(keepData);
            return keep;
        }

        internal string DestroyKeep(int keepId, string userId)
        {
            Keep keep = GetKeepById(keepId);

            if (keep.CreatorId != userId)
            {
                throw new Exception("Not your Keep to Delete!");
            }
            _keepsRepository.DestroyKeep(keepId);
            return $"{keep.Name} has been Deleted!";
        }

        internal Keep GetKeepById(int keepId)
        {
            Keep keep = _keepsRepository.GetKeepById(keepId);

            if (keep == null)
            {
                throw new Exception($"{keepId} is invalid!");
            }
            return keep;

        }
        internal List<Keep> GetKeeps()
        {
            List<Keep> keeps = _keepsRepository.GetKeeps();
            return keeps;
        }


        internal List<Keep> GetKeepsByProfileId(string profileId, string userId)
        {

            List<Keep> keeps = _keepsRepository.GetKeepsByProfileId(profileId, userId);
            return keeps;
        }

        internal Keep UpdateKeep(int keepId, string userId, Keep keepData)
        {
            Keep ogKeep = GetKeepById(keepId);

            if (ogKeep.CreatorId != userId)
            {
                throw new Exception("Not your Keep to Update");
            }
            ogKeep.Name = keepData.Name ?? ogKeep.Name;
            ogKeep.Description = keepData.Description ?? ogKeep.Description;

            Keep keep = _keepsRepository.UpdateKeep(ogKeep);
            return keep;
        }
    }

}
