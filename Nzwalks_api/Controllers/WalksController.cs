using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nzwalks_api.CustomActionFilter;
using Nzwalks_api.Models.DTO;
using Nzwalks_api.Models.DTO.Requestes;
using Nzwalks_api.Repostories.Interfaces;

namespace Nzwalks_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IWalkRepository _walkRepository;

    
        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            _walkRepository = walkRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllWalks([FromQuery] string?filterOn,
            [FromQuery] string?filterQuery,
            [FromQuery] String? sortBy, [FromQuery] bool IsAsc=true,
            [FromQuery]int PageNumber = 1, [FromQuery]int pageSize=1000)
        {
            var walks = await _walkRepository.GetAllWalks(filterOn,filterQuery,
                sortBy,IsAsc,PageNumber,pageSize);
            var mapresult =new Dictionary<String,object> {{"result",_mapper.Map<List<WalkDto>>(walks) }};
                return Ok(mapresult);  
        }
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetWalkById([FromRoute] Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid ID.");
            }
            var walk = await _walkRepository.GetById(id);
            if (walk == null)
            {
                return NotFound($"Walk with ID {id} not found.");
            }
            return Ok(_mapper.Map<WalkDto>(walk));
        }
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateWalk([FromRoute] Guid id, [FromBody] UpdateWalkRequestDto walkRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var walk = _mapper.Map<Models.Domain.Walk>(walkRequestDto);
            var updatedWalk = await _walkRepository.UpadateWalk(id, walk);
            if (updatedWalk == null)
            {
                return NotFound($"Walk with ID {id} not found.");
            }
            return Ok(_mapper.Map<WalkDto>(updatedWalk));
        }
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteWalk([FromRoute] Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid ID.");
            }
            var walk = await _walkRepository.DeleteWalk(id);
            if (walk == null)
            {
                return NotFound($"Walk with ID {id} not found.");
            }
            return Ok(_mapper.Map<WalkDto>(walk));
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> CreateWalk([FromBody] AddWalkRequestDto walkRequestdto)
        {
           
                var walk = _mapper.Map<Models.Domain.Walk>(walkRequestdto);
                walk = await _walkRepository.addwalk(walk);
                return Ok(_mapper.Map<WalkDto>(walk));
            
           
        }
    }
}
