using System.ComponentModel.DataAnnotations.Schema;

namespace Nzwalks_api.Models.Domain
{
    public class Walk
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public String? WalkImageUrl { get; set; }
        public Guid RegionId { get; set; }
        public Guid DiffculatyId { get; set; }
        [ForeignKey(nameof(RegionId))]
        public Region region { get; set; }
        [ForeignKey(nameof(DiffculatyId))]

        public Diffculty diffculty { get; set; }

    }
}
