using Entity.DTO;
using Entity.Model.Security;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Interface
{
    public interface IRoleViewBusiness
    {
        Task Delete(int id);
        Task<IEnumerable<RoleViewDto>> GetAll();
        Task<RoleViewDto> GetById(int id);
        Task<RoleView> Save(RoleViewDto entity);
        Task Update(RoleViewDto entity);
    }
}
