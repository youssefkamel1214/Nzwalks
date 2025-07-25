namespace Nzwalks_api.Models.Domain
{
    public class Region
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        // Navigation property
        public string? ImageUrl { get; set; }

    }
}
