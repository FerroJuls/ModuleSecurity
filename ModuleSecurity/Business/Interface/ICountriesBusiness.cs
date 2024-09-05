using Entity.DTO;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Threading.Tasks;

namespace Business.Interface
{
    public interface ICountryBusiness
    {
        Task<IEnumerable<CountryDto>> GetAll();
        Task<CountryDto> GetById(int id);
        Task<Country> Save(CountryDto entity);
        Task Update(CountryDto entity);
        Task Delete(int id);
    }
}
