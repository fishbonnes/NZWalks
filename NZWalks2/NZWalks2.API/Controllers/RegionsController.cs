using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks2.API.Models.Domain;
using NZWalks2.API.Repositories;

namespace NZWalks2.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegionsController : Controller
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult>GettAllRegions()
        {
            var regions = await regionRepository.GetallAsync();

            // Return DTO regions
            //var regionsDTO = new List<Models.DTO.Region>();
            //regions.ToList().ForEach(reggion =>
            //{
            //    var regionDTO = new Models.DTO.Region()
            //    {
            //        Id = reggion.Id,
            //        Code = reggion.Code,
            //        Name = reggion.Name,
            //        Area = reggion.Area,
            //        Lat = reggion.Lat,
            //        Long = reggion.Long,
            //        Population = reggion.Population

            //    };

            //    regionsDTO.Add(regionDTO);
            //});
            var regionsDTO = mapper.Map<List<Models.DTO.Region>>(regions);

            return Ok(regionsDTO);
        }
    }
}
