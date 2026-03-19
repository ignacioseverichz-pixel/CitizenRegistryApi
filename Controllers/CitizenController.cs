using CitizenRegistryApi.Models;
using CitizenRegistryApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CitizenRegistryApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CitizenController : ControllerBase
    {
        private readonly CitizenService _citizenService;

        public CitizenController(CitizenService citizenService)
        {
            _citizenService = citizenService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Citizen>> GetAll()
        {
            return Ok(_citizenService.GetAll());
        }

        [HttpGet("{ci}")]
        public ActionResult<Citizen> GetByCi(string ci)
        {
            var citizen = _citizenService.GetByCi(ci);

            if (citizen == null)
                return NotFound("Citizen not found");

            return Ok(citizen);
        }

        [HttpPost]
        public async Task<ActionResult<Citizen>> Create([FromBody] CreateCitizenRequest request)
        {
            var result = await _citizenService.CreateAsync(request);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Citizen);
        }

        [HttpPut("{ci}")]
        public ActionResult<Citizen> Update(string ci, [FromBody] UpdateCitizenRequest request)
        {
            var result = _citizenService.Update(ci, request);

            if (!result.Success)
                return NotFound(result.Message);

            return Ok(result.Citizen);
        }

        [HttpDelete("{ci}")]
        public IActionResult Delete(string ci)
        {
            var result = _citizenService.Delete(ci);

            if (!result.Success)
                return NotFound(result.Message);

            return Ok(result.Message);
        }
    }
}