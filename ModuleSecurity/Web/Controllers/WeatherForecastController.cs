using Entity.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace Web.Controllers.Implements
{
    [ApiController]
    [Route("[controller]")]
    public class ModuleController : ControllerBase
    {
        private readonly IModuleBusiness _moduleBusiness;

        public ModuleController(IModuleBusiness moduloBusiness)
        {
            _moduleBusiness = moduloBusiness;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ModuleDto>>> GetAll()
        {
            var result = await _moduleBusiness.GetAll();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ModuloDto>> GetById(int id)
        {
            var result = await _moduleBusiness.GetById(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);

            [HttpPost]
            public async Task<ActionResult<Modulo>> Save([FromBody] ModuloDto entity)
            {
                if (entity == null)
                {
                    return BadRequest("Entity is null");
                }

                var result = await _moduleBusiness.Save(entity);
                return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
            }

            [HttpPut("{id}")]
            public async Task<IActionResult> Update([FromBody] ModuloDto entity)
            {
                if (entity == null || entity.Id == 0)
                {
                    return BadRequest();
                }

                await _moduleBusiness.Update(entity);
                return NoContent();
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> Delete(int id)
            {
                await _moduleBusiness.Delete(id);
                return NoContent();
            }
        }