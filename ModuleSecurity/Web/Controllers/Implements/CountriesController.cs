using Business.Interfaces;
using Entity.DTO;
using Entity.Model.Security;
using Microsoft.AspNetCore.Mvc;


namespace Web.Controllers.Implements
{
    [ApiController]
    [Route("[controller]")]
    public class CountriesController : ControllerBase
    {
        private readonly ICountriesBusiness _countryBusiness;

        public CountriesController(ICountriesBusiness countriesBusiness)
        {
            _countryBusiness = countriesBusiness;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CountriesDto>>> GetAll()
        {
            var result = await _countryBusiness.GetAll();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CountriesDto>> GetById(int id)
        {
            var result = await _countryBusiness.GetById(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<Countries>> Save([FromBody] CountriesDto countriesDto)
        {
            if (countriesDto == null)
            {
                return BadRequest("Entity is null");
            }

            var result = await _countryBusiness.Save(countriesDto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] CountriesDto countriesDto)
        {
            if (countriesDto == null || countriesDto.Id == 0)
            {
                return BadRequest();
            }

            await _countryBusiness.Update(countriesDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _countryBusiness.Delete(id);
            return NoContent();
        }
    }
}
