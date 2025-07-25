using Nzwalks_api.Data;
using Nzwalks_api.Models.Domain;
using Nzwalks_api.Repostories.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace Nzwalks_api.Repostories
{
    public class SqlWalkRepository : IWalkRepository
    {
        private readonly NzwalksDbContext _context;

        public SqlWalkRepository(NzwalksDbContext context)
        {
            _context = context;
        }

        public async Task<Walk> addwalk(Walk walk)
        {
           
            if (walk != null)
            {
               await _context.Walks.AddAsync(walk);
               await _context.SaveChangesAsync();
            }
                return walk;

            
       
        }

        public async Task<Walk?> DeleteWalk(Guid guid)
        {
            var walk = await _context.Walks.FirstOrDefaultAsync(x => x.Id == guid);
            if (walk != null)
            {
                _context.Walks.Remove(walk);
                await _context.SaveChangesAsync();
            }
            return walk;
        }

        public async Task<List<Walk>> GetAllWalks(string? filterOn, string? filterQuery, 
            string? sortBy, bool isAsc, int pageNumber, int pageSize)
        {
            //Filtering logic
            var walks =   _context.Walks.Include("diffculty").Include("region").AsQueryable();
            if (!string.IsNullOrWhiteSpace(filterOn) &&
                !string.IsNullOrWhiteSpace(filterQuery)) 
            {
                if (filterOn.ToLower() == "name")
                {
                    walks = walks.Where(x => x.Name.ToLower().Contains(filterQuery.ToLower()));
                }
                else if (filterOn.ToLower() == "description")
                {
                     walks = walks.Where(x => x.Description.ToLower().Contains(filterQuery.ToLower()));
                }
                
            }
            //Sorting logic
            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                if (sortBy.ToLower() == "name")
                {
                    walks = isAsc ? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x => x.Name);
                }
                else if (sortBy.ToLower() == "lengthinkm")
                {
                    walks = isAsc ? walks.OrderBy(x => x.LengthInKm) : walks.OrderByDescending(x => x.LengthInKm);
                }
            }
            //Pagination logic
             walks = walks.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            return await walks.ToListAsync();
        }

        public async Task<Walk?> GetById(Guid guid)
        {
            var walk = await _context.Walks.
                Include("diffculty").Include("region").
                FirstOrDefaultAsync(x => x.Id == guid);
            //var walk = await _context.Walks.FirstOrDefaultAsync(x => x.Id == guid);
            return walk;
        }


        public async Task<Walk?> UpadateWalk(Guid guid, Walk updatedWalk)
        {
            var walk = await _context.Walks.FirstOrDefaultAsync(x => x.Id == guid);
            if (walk != null)
            {
                walk.Name = updatedWalk.Name;   
                walk.LengthInKm = updatedWalk.LengthInKm;
                walk.RegionId = updatedWalk.RegionId;
                walk.DiffculatyId = updatedWalk.DiffculatyId;
                walk.Description = updatedWalk.Description;
                walk.WalkImageUrl = updatedWalk.WalkImageUrl;
                await _context.SaveChangesAsync();
            }
            return walk;
        }
    }
}
