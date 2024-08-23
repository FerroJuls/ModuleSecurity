using Entity.Model.Security;

namespace Data.Interfaces
{
    public interface IModuleData
    {
        public Task Delete(int id);
        public Task<Module> GetById(int id);
        public Task<Module> Save(Module entity);
        public Task Update(Module entity);
        public Task<Module> GetByName(string description);
    }
}
