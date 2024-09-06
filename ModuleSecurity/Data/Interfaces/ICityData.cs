using Entity.DTO;
using Entity.Model.Security;


namespace Data.Interfaces
{
    public interface ICityData
    {
        Task<City> GetById(int id);
        Task<City> GetByName(string name);
        Task<City> Save(City entity);
        Task Update(City entity);
        Task Delete(int id);
        Task<IEnumerable<City>> GetAll();
        Task<IEnumerable<DataSelectDto>> GetAllSelect();
    }
}
