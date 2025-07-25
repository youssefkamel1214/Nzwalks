using Nzwalks_api.Models.Domain;

namespace Nzwalks_api.Repostories.Interfaces
{
    public interface IWalkRepository
    {
        public Task<Walk> addwalk(Walk walk);
        public Task<Walk?> GetById(Guid guid);
        public Task<Walk?> UpadateWalk(Guid guid, Walk updatedWalk);
        public Task<Walk?> DeleteWalk(Guid guid);
        public Task<List<Walk>> GetAllWalks(string? filterOn,
            string? filterQuery, string? sortBy, bool isAsc,
            int pageNumber, int pageSize);
    }
}
