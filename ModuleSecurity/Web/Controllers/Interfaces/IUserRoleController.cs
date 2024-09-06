using Entity.DTO;
using Entity.Model.Security;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.Interfaces
{
    public interface IUserRoleController
    {
        Task<IActionResult> Delete(int id);
        Task<ActionResult<UserRoleDto>> GetById(int id);
        Task<ActionResult<UserRole>> Save([FromBody] UserRoleDto userroleDto);
        Task<IActionResult> Update([FromBody] UserRoleDto userroleDto);
        Task<ActionResult<IEnumerable<UserRoleDto>>> GetAll();
    }
}
