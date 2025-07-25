namespace Nzwalks_api.Models.DTO
{
    public class WalkDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public String? WalkImageUrl { get; set; }
        public Guid RegionId { get; set; }
        public Guid DiffculatyId { get; set; }
        public RegionDto Region { get; set; }
        public DiffcultyDto Diffculty { get; set; }
    }
}
