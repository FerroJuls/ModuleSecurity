using Entity.DTO;
using Entity.Model.Security;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.Interfaces
{
    public interface IStateController
    {
        Task<IActionResult> Delete(int id);
        Task<ActionResult<StateDto>> GetById(int id);
        Task<ActionResult<State>> Save([FromBody] StateDto stateDto);
        Task<IActionResult> Update([FromBody] StateDto stateDto);
        Task<ActionResult<IEnumerable<StateDto>>> GetAll();
    }
}
