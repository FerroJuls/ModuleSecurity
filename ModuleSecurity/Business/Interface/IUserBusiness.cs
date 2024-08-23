using Entity.DTO;
using Entity.Model.Security;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Interface
{
    public interface IUserBusiness
    {
        Task Delete(int id);
        Task<IEnumerable<UserDto>> GetAll();
        Task<UserDto> GetById(int id);
        Task<User> Save(UserDto entity);
        Task Update(UserDto entity);
    }
}
