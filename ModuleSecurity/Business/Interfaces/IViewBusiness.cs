
using Entity.DTO;
using Entity.Model.Security;

namespace Business.Interfaces
{
    public interface IViewBusiness
    {
        Task Delete(int id);
        Task<ViewDto> GetById(int id);
        Task<View> Save(ViewDto entity);
        Task Update(ViewDto entity);
        Task<IEnumerable<ViewDto>> GetAll();

        //Task<IEnumerable<DataSelectDto>> GetAllSelect();

       //* Task<View> GetByName(string name)*/;
    }
}
