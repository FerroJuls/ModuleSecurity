using Entity.DTO;
using Entity.Model.Security;


namespace Business.Interfaces
{
    public interface IUserBusiness
    {
        Task Delete(int id);
        Task<UserDto> GetById(int id);
        Task<User> Save(UserDto entity);
        Task Update(UserDto entity);
        Task<IEnumerable<UserDto>> GetAll();

        //Task<IEnumerable<DataSelectDto>> GetAllSelect();
        Task<User> GetByName(string name);
    }
}
