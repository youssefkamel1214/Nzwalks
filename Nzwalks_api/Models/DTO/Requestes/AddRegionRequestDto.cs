using System.ComponentModel.DataAnnotations;

namespace Nzwalks_api.Models.DTO.Requestes
{
    public class AddRegionRequestDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [MaxLength(3)]
        [MinLength(3)]
        public string Code { get; set; }
        // Navigation property
        public string? ImageUrl { get; set; }
    }
}
