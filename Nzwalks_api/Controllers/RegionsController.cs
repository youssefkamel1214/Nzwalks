using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Nzwalks_api.Data;
using Nzwalks_api.Models.Domain;
using Nzwalks_api.Models.DTO;
using Nzwalks_api.Models.DTO.Requestes;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Nzwalks_api.Repostories.Interfaces;
using Nzwalks_api.CustomActionFilter;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;

namespace Nzwalks_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<RegionsController> _logger;
        public RegionsController(IRegionRepository regionRepository,
            IMapper mapper,
            ILogger<RegionsController> logger)
        {
            _regionRepository = regionRepository;
            _mapper = mapper;
            _logger = logger;
        }
        [HttpGet]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> getAll()
        {
            
            var regions = await _regionRepository.GetAllAsync();
            List<RegionDto> regionDtos =   _mapper.Map<List<RegionDto>>(regions);
            return Ok(regionDtos);
        }
        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles ="Reader")]
        public async Task<IActionResult> getbyid([FromRoute] Guid id)
        {
            Console.WriteLine($"Received request for region with ID: {id}");
            var region = await _regionRepository.FindById(id);
            if (region == null)
            {
                return NotFound();
            }
            var regionDto = _mapper.Map<RegionDto>(region);
            return Ok(regionDto);
        }
        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> CreateRegion([FromBody] AddRegionRequestDto regionDto)
        {
           
            var region =_mapper.Map<Region>(regionDto);
            region=  await _regionRepository.AddRegion(region);
            var regionDtoResponse = _mapper.Map<RegionDto>(region);
            return CreatedAtAction(nameof(getbyid), new { id = regionDtoResponse.Id }
            , regionDtoResponse);
        }
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> UpdateRegion([FromRoute] Guid id,
            [FromBody] UpdateRegionRequestDto requestDto ) 
        {
            var region = await _regionRepository.UpdateRegion(id,
                _mapper.Map<Region>(requestDto));
            if (region == null)
            {
                return NotFound();
            }
            var regionDtoResponse = _mapper.Map<RegionDto>(region);
            return Ok(regionDtoResponse);
        }
        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer,Reader")]
        public async Task<IActionResult> DeleteRegion([FromRoute] Guid id)
        {
            var region = await _regionRepository.DeleteRegion(id);
            if (region == null)
            {
                return NotFound();
            }
            RegionDto regionDtoResponse = _mapper.Map<RegionDto>(region);
            return Ok(regionDtoResponse);
        }
    }
}
