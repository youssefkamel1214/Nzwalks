using Nzwalks_api.Models.Domain;

namespace Nzwalks_api.Repostories.Interfaces
{
    public interface ImageRepository
    {
        public Task<Image> UploadImageAsync(Image image);
    }
}
