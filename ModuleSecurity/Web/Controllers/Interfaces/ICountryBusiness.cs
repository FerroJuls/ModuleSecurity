using Data.Implements;
using Entity.DTO;
using Entity.Model.Security;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Threading.Tasks;

namespace Web.Controllers.Interfaces
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
