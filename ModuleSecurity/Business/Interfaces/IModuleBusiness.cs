using Entity.DTO;
using Entity.Model.Security;

namespace Business.Interfaces
{
    public interface IModuleBusiness
    {
        Task Delete(int id);
        Task<ModuleDto> GetById(int id);
        Task<Module> Save(ModuleDto entity);
        Task Update(ModuleDto entity);
        Task<IEnumerable<ModuleDto>> GetAll();

        //Task<IEnumerable<DataSelectDto>> GetAllSelect();

        //* Task<View> GetByName(string name)*/;
    }
}
