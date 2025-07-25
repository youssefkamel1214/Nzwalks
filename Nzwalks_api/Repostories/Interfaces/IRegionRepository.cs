using Nzwalks_api.Models.Domain;

namespace Nzwalks_api.Repostories.Interfaces
{
    public interface IRegionRepository
    {
     Task<List<Region>>  GetAllAsync();
     Task<Region?> FindById(Guid guid);
     Task<Region> AddRegion(Region region);
     Task<Region?> UpdateRegion(Guid guid,Region region);
     Task<Region?> DeleteRegion(Guid id);

    }
}
