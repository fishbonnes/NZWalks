using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks2.API.Models.Domain;
using NZWalks2.API.Models.DTO;
using NZWalks2.API.Repositories;

namespace NZWalks2.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalksController : Controller
    {
        private readonly IWalkRepository walkRepository;
        private readonly IMapper mapper;

        public WalksController(IWalkRepository walkRepository, IMapper mapper) 
        {
            this.walkRepository = walkRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllWalksAsync()
        {
            var walks = await walkRepository.GetallAsync();

            // Return DTO walks
            //var walksDTO = new List<Models.DTO.Walk>();
            
            //walks.ToList().ForEach(walk =>
            //{
            //    var walkDTO = new Models.DTO.Walk()
            //    {
            //        Id = walk.Id,
            //        Name = walk.Name,
            //        Length = walk.Length,
            //        RegionId = walk.RegionId,
            //        WalkDifficultyId = walk.WalkDifficultyId
                    
            //    };
            //    walksDTO.Add(walkDTO);
            //});     
            var walksDTO = mapper.Map<List<Models.DTO.Walk>>(walks);
            
            return Ok(walksDTO);
        }
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkAsync")]
        public async Task<IActionResult> GetWalkAsync(Guid id)
        {
            var walk = await walkRepository.GetAsync(id);
            if (walk == null)
            {
                return NotFound();
            }
            //Convert back to DTO
            //var walkDTO = new Models.DTO.Walk()
            //{
            //    Id = walk.Id,
            //    Name = walk.Name,
            //    Length = walk.Length,
            //    RegionId = walk.RegionId,
            //    WalkDifficultyId = walk.WalkDifficultyId
            //};
            var walkDTO = mapper.Map<Models.DTO.Walk>(walk);
            return Ok(walkDTO);
        }
        [HttpPost]
        public async Task<IActionResult> AddWalkAsync(Models.DTO.AddWalkRequest addWalkRequest)
        {
            // Request(DTO) to Domain model
            var walk = new Models.Domain.Walk()
            {
                Name = addWalkRequest.Name,
                Length = addWalkRequest.Length,
                RegionId = addWalkRequest.RegionId,
                WalkDifficultyId = addWalkRequest.WalkDifficultyId
            };
            //Pass details to Repository 
            walk = await walkRepository.AddAsync(walk);

            //Convert back to DTO
            var walkDTO = new Models.DTO.Walk()
            {
                Id = walk.Id,
                Name = walk.Name,
                Length = walk.Length,
                RegionId = walk.RegionId,
                WalkDifficultyId = walk.WalkDifficultyId
            };
            return CreatedAtAction(nameof(GetWalkAsync), new { id = walkDTO.Id }, walkDTO);
        }
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteWalkAsync(Guid id)
        {
            // Get region from Database
            var walk = await walkRepository.DeleteAsync(id);
            if(walk == null)
            {
                return NotFound();
            }
            // Convert back reposnse back to DTO
            var walkDTO = new Models.DTO.Walk()
            {
                Id = walk.Id,
                Name = walk.Name,
                Length = walk.Length,
                RegionId = walk.RegionId,
                WalkDifficultyId = walk.WalkDifficultyId
            };
            return Ok(walkDTO); 
        }
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalksAsync([FromRoute] Guid id, [FromBody] Models.DTO.UpdateWalkRequest updateWalkRequest)
        {
            // Convert DTO to Domain model
            var walk = new Models.Domain.Walk()
            {
                Name = updateWalkRequest.Name,
                Length = updateWalkRequest.Length,
                RegionId = updateWalkRequest.RegionId,
                WalkDifficultyId = updateWalkRequest.WalkDifficultyId
            };

            // Update region using repository
            walk = await walkRepository.UpdateAsync(id, walk);

            //If Null then Not Found
            if (walk == null)
            {
                return NotFound();
            }

            // Convert Back to DTO
            var walkDTO = new Models.DTO.Walk()
            {
                Id = walk.Id,
                Name = walk.Name,
                Length = walk.Length,
                RegionId = walk.RegionId,
                WalkDifficultyId = walk.WalkDifficultyId
            };
            //Return Ok Response
            return Ok(walkDTO);
        }
    }
}
