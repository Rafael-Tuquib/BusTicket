using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BusTicketing.Services.Interfaces;

namespace BusTicketing.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class LocationController : Controller
    {
        private readonly ILocationService _locationService;
        public LocationController(ILocationService locationService) { _locationService = locationService; }

        [HttpGet]
        public async Task<IActionResult> GetBarangays(int municipalityId)
        {
            if (municipalityId <= 0) return BadRequest(new { error = "MunicipalityId is required" });
            var list = (await _locationService.GetBarangaysByMunicipalityAsync(municipalityId)).Select(b => new { id = b.Id, name = b.Name }).ToList();
            return Ok(list);
        }
    }
}