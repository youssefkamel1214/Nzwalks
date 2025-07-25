using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Nzwalks_api.Models.DTO
{
  
    public class RegionDto 
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Code { get; set; }
        // Navigation property
        public string? ImageUrl { get; set; }
    }
}
