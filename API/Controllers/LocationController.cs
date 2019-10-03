using API.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILocationRepository _locationRepo;

        public LocationController(ILocationRepository LocationRepo)
        {
            _locationRepo = LocationRepo;
        }

        [HttpGet("countries")]
        public IActionResult GetCountries() {
            // await System.Threading.Tasks.Task.Delay(3000);
            return Ok(_locationRepo.getCountries());
        }

        [HttpGet("states/{countryId}")]
        public IActionResult GetStates(int countryId) {
            return Ok(_locationRepo.getStates(countryId));
        }
    }
}