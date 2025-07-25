using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Nzwalks_api.CustomActionFilter;
using Nzwalks_api.Models.Domain;
using Nzwalks_api.Models.DTO.Requestes;
using Nzwalks_api.Repostories.Interfaces;

namespace Nzwalks_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly ImageRepository _imageRepository;

        private readonly IMapper _mapper;

        public ImagesController(ImageRepository imageRepository, IMapper mapper)
        {
            _imageRepository = imageRepository;
            _mapper = mapper;
        }

        // post image
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm]ImageUploadRequestDto request )
        {
            validateFileUpload(request);
            if (ModelState.IsValid)
            {
                var imagedomainmodel = _mapper.Map<Image>(request);
                var reslut = await _imageRepository.UploadImageAsync(imagedomainmodel);
                return Ok(reslut);
            }
            return BadRequest(ModelState);
        }
        private void validateFileUpload(ImageUploadRequestDto request)
        {
           
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var fileExtension = Path.GetExtension(request.File.FileName).ToLowerInvariant();
            if (!allowedExtensions.Contains(fileExtension))
            {
                ModelState.AddModelError("File", "Unsupported File Type" );
            }
            if (request.File.Length > (10 * 1024 * 1024))
            {
                ModelState.AddModelError("File", "File size exceeds 10 MB limit");
            }
            if (string.IsNullOrWhiteSpace(request.FileName))
            {
                ModelState.AddModelError("FileName", "File name is required");
            }
        }
    }
}
