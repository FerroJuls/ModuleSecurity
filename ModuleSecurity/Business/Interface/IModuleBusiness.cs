using Entity.DTO;
using Entity.Model.Security;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Interface
{
    public interface IModuleBusiness
    {
        Task Delete(int id);
        Task<IEnumerable<ModuleDto>> GetAll();
        Task<ModuleDto> GetById(int id);
        Task<Module> Save(ModuleDto entity);
        Task Update(ModuleDto entity);
    }
}
