using Entity.DTO;
using Entity.Model.Security;

namespace Business.Interfaces
{
    public interface IRoleViewBusiness
    {
        Task Delete(int id);
        Task<RoleViewDto> GetById(int id);
        Task<RoleView> Save(RoleViewDto entity);
        Task Update(RoleViewDto entity);
        Task<IEnumerable<RoleViewDto>> GetAll();

        //Task<IEnumerable<DataSelectDto>> GetAllSelect();

        //* Task<View> GetByName(string name)*/;
    }
}
