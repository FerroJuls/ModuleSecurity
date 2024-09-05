using Entity.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Interface
{
    public interface ICityBusiness
    {
        Task<IEnumerable<CityDto>> GetAll();
        Task<CityDto> GetById(int id);
        Task<City> Save(CityDto entity);
        Task Update(CityDto entity);
        Task Delete(int id);
    }
}
