using Entity.DTO;
using Entity.Model.Security;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface ICountryData
    {
        Task<Country> GetById(int id);
        Task<Country> GetByName(string name);
        Task<Country> Save(Country entity);
        Task Update(Country entity);
        Task Delete(int id);
        Task<IEnumerable<Country>> GetAll();
        Task<IEnumerable<DataSelectDto>> GetAllSelect();
    }
}
