using Entity.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Interface
{
    public interface IViewBusiness
    {
        Task Delete(int id);
        Task<IEnumerable<ViewDto>> GetAll();
        Task<ViewDto> GetById(int id);
        Task<View> Save(ViewDto entity);
        Task Update(ViewDto entity);
    }
}
