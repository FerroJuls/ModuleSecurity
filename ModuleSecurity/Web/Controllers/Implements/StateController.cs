using Business.Interfaces;
using Entity.DTO;
using Entity.Model.Security;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.Implements
{
    [ApiController]
    [Route("[controller]")]
    public class StateController : ControllerBase
    {
        private readonly IStateBusiness _stateBusiness;

        public StateController(IStateBusiness stateBusiness)
        {
            _stateBusiness = stateBusiness;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StateDto>>> GetAll()
        {
            var result = await _stateBusiness.GetAll();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StateDto>> GetById(int id)
        {
            var result = await _stateBusiness.GetById(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<State>> Save([FromBody] StateDto stateDto)
        {
            if (stateDto == null)
            {
                return BadRequest("Entity is null");
            }

            var result = await _stateBusiness.Save(stateDto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] StateDto stateDto)
        {
            if (stateDto == null || stateDto.Id == 0)
            {
                return BadRequest();
            }

            await _stateBusiness.Update(stateDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _stateBusiness.Delete(id);
            return NoContent();
        }
    }
}
