using Microsoft.EntityFrameworkCore;
using Nzwalks_api.Data;
using Nzwalks_api.Models.Domain;
using Nzwalks_api.Repostories.Interfaces;
using System;

namespace Nzwalks_api.Repostories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly NzwalksDbContext _context;
        public SQLRegionRepository(NzwalksDbContext context)
        {
            _context = context;
        }

        public async Task<Region> AddRegion(Region region)
        {
            await  _context.Regions.AddAsync(region);
            await _context.SaveChangesAsync();
            return region;
        }

        public async Task<Region?> DeleteRegion(Guid id)
        {
            var region = await _context.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (region != null)
            {
                _context.Regions.Remove(region);
                await _context.SaveChangesAsync();
            }
            return region;
        }

        public async Task<Region?> FindById(Guid guid)
        {
           var region = await _context.Regions.FirstOrDefaultAsync(x=>x.Id==guid);
           return region;
        }

        public async Task<List<Region>> GetAllAsync()
        {
            List<Region> regions = await _context.Regions.ToListAsync();
            return regions;
        }

   

        public async Task<Region?> UpdateRegion(Guid guid, Region region)
        {
            var existingRegion = await _context.Regions.
                FirstOrDefaultAsync(x => x.Id == guid);
            if (existingRegion!=null)
            {
                existingRegion.Code = region.Code;
                existingRegion.Name = region.Name;
                existingRegion.ImageUrl = region.ImageUrl;
                await _context.SaveChangesAsync();
            }
            return existingRegion;
        }
    }
}
