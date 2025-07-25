using Nzwalks_api.Data;
using Nzwalks_api.Models.Domain;
using Nzwalks_api.Repostories.Interfaces;

namespace Nzwalks_api.Repostories
{
    public class LocalImageRepository : ImageRepository
    {
        private readonly  IWebHostEnvironment _webHostEnvironment;
        private readonly  IHttpContextAccessor _httpContextAccessor;
        private readonly NzwalksDbContext _nzwalksDbContext;

        public LocalImageRepository(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor, NzwalksDbContext nzwalksDbContext)
        {
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
            _nzwalksDbContext = nzwalksDbContext;
        }

        public async Task<Image> UploadImageAsync(Image image)
        {
            string localPath =Path.Combine(_webHostEnvironment.ContentRootPath, "images", image.FileName+image.FileExtension);

            //upload Image to Local File System
            var stream =new FileStream(localPath, FileMode.Create);
            await image.File.CopyToAsync(stream);
            var urlfilePath = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}" +
                $"{_httpContextAccessor.HttpContext.Request.PathBase}/Images/{image.FileName}{image.FileExtension}";
            image.FilePath = urlfilePath;

            //save image to database
            await _nzwalksDbContext.Images.AddAsync(image);
            await _nzwalksDbContext.SaveChangesAsync();
            return image;

        }
    }
}
