using Entity.DTO;
using Entity.Model.Security;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.Interfaces
{
    public interface IModuleController
    {
        Task<IActionResult> Delete(int id);
        Task<ActionResult<ModuleDto>> GetById(int id);
        Task<ActionResult<Module>> Save([FromBody] ModuleDto moduleDto);
        Task<IActionResult> Update([FromBody] ModuleDto moduleDto);
        Task<ActionResult<IEnumerable<ModuleDto>>> GetAll();
    }
}
