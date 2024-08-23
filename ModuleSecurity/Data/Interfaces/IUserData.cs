using Entity.Model.Security;

namespace Data.Interfaces
{
    public interface IUserData
    {
        public Task Delete(int id);
        public Task<User> GetById(int id);
        public Task<User> Save(User entity);
        public Task Update(User entity);
        public Task<User> GetByName(string username);
    }
}
