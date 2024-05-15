﻿using Microsoft.AspNetCore.Mvc;
using NZWalks2.API.Data;
using NZWalks2.API.Models.DTO;
using NZWalks2.API.Repositories;

namespace NZWalks2.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalkDifficultyController : Controller
    {
        private readonly IWalkDifficultyRepository walkDifficultyRepository;

        public WalkDifficultyController(IWalkDifficultyRepository walkDifficultyRepository)
        {
            this.walkDifficultyRepository = walkDifficultyRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllWalkDifficultiesAsync()
        {
            var walkDifficulties = await walkDifficultyRepository.GetallAsync();
            // Return DTO walks
            var walkDifficultiesDTO = new List<Models.DTO.WalkDifficulty>();
            walkDifficulties.ToList().ForEach(walkDifficulty =>
            {
                var walkDifficultyDTO = new Models.DTO.WalkDifficulty()
                {
                    Id = walkDifficulty.Id,
                    Code = walkDifficulty.Code
                };
                walkDifficultiesDTO.Add(walkDifficultyDTO);
            });

            return Ok(walkDifficultiesDTO);
        }
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkDifficultyAsync")]
        public async Task<IActionResult> GetWalkDifficultyAsync(Guid id)
        {
            var walkDifficulty = await walkDifficultyRepository.GetAsyncId(id);
            if (walkDifficulty == null) 
            {
                return NotFound();
            }
            //Convert back to DTO
            var walkDifficultyDTO = new Models.DTO.WalkDifficulty()
            {
                Id = walkDifficulty.Id,
                Code = walkDifficulty.Code
            };
            return Ok(walkDifficultyDTO);
        }
        [HttpPost]
        public async Task<IActionResult> AddWalkDifficultyAsinc(Models.DTO.AddWalkDifficultyRequest addWalkDifficultyRequest)
        {
            // Request(DTO) to Domain model
            var walkDifficulty = new Models.Domain.WalkDifficulty()
            {
                Code = addWalkDifficultyRequest.Code
            };
            //Pass details to Repository 
            walkDifficulty = await walkDifficultyRepository.AddAsync(walkDifficulty);

            //Convert back to DTO
            var walkDifficultyDTO = new Models.DTO.WalkDifficulty()
            {
                Id = walkDifficulty.Id,
                Code = walkDifficulty.Code
            };

            return CreatedAtAction(nameof(GetWalkDifficultyAsync), new { id = walkDifficultyDTO.Id }, walkDifficultyDTO);
        }
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalkDifficultyAsync([FromRoute] Guid id, [FromBody] Models.DTO.UpdateWalkDifficultyRequest updateWalkDifficultyRequest)
        {
            // Convert DTO to Domain model
            var walkDifficulty = new Models.Domain.WalkDifficulty()
            {
                Code = updateWalkDifficultyRequest.Code
            };
            // Update region using repository
            walkDifficulty = await walkDifficultyRepository.UpdateAsync(id, walkDifficulty);

            //If Null then Not Found
            if (walkDifficulty == null) 
            {
                return NotFound();
            }
            // Convert Back to DTO
            var walkDifficultyDTO = new Models.DTO.WalkDifficulty()
            {
                Id = walkDifficulty.Id,
                Code = walkDifficulty.Code
            };
            return Ok(walkDifficultyDTO);
        }
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteWalkDifficultyAsync(Guid id)
        {
            // Get region from Database
            var walkDifficulty = await walkDifficultyRepository.DeleteAsync(id);

            if (walkDifficulty == null)
            {
                return NotFound();
            }
            // Convert back reposnse back to DTO
            var walkDifficultyDTO = new Models.DTO.WalkDifficulty()
            {
                Id = walkDifficulty.Id,
                Code = walkDifficulty.Code
            };
            return Ok(walkDifficultyDTO);
        }
    }
}