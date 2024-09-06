using Entity.Model.Security;

namespace Data.Interfaces
{
    public interface IRoleData
    {
        public Task Delete(int id);
        public Task<Role> GetById(int id);
        public Task<Role> Save(Role entity);
        public Task Update(Role entity);
        public Task<IEnumerable<Role>> GetAll();
        public Task<Role> GetByName(string name);
    }
}
