

using Entity.Model.Security;

namespace Data.interfaces
{
    public interface IModuleData
    {
        Task Delete(int id);
        Task<Module> GetById(int id);
        Task<Module> Save(Module entity);
        Task Update(Module entity);

        Task<IEnumerable<Module>> GetAll();

        //Task<IEnumerable<DataSelectDto>> GetAllSelect();

        //Task<Module> GetByName(string name);
    }
}
