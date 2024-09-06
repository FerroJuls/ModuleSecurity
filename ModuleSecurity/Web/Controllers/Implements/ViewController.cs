using Business.Interfaces;
using Entity.DTO;
using Entity.Model.Security;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.Implements
{
    [ApiController]
    [Route("[controller]")]
    public class ViewController : ControllerBase
    {
        private readonly IViewBusiness _viewBusiness;

        public ViewController(IViewBusiness viewBusiness)
        {
            _viewBusiness = viewBusiness;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ViewDto>>> GetAll()
        {
            var result = await _viewBusiness.GetAll();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ViewDto>> GetById(int id)
        {
            var result = await _viewBusiness.GetById(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<View>> Save([FromBody] ViewDto viewDto)
        {
            if (viewDto == null)
            {
                return BadRequest("Entity is null");
            }

            var result = await _viewBusiness.Save(viewDto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] ViewDto viewDto)
        {
            if (viewDto == null || viewDto.Id == 0)
            {
                return BadRequest();
            }

            await _viewBusiness.Update(viewDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _viewBusiness.Delete(id);
            return NoContent();
        }
    }
}
