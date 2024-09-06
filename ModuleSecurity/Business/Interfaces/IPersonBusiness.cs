using Entity.DTO;
using Entity.Model.Security;


namespace Business.Interfaces
{
    public interface IPersonBusiness
    {
        Task Delete(int id);
        Task<PersonDto> GetById(int id);
        Task<Personcs> Save(PersonDto entity);
        Task Update(PersonDto entity);
        Task<IEnumerable<PersonDto>> GetAll();

        //Task<IEnumerable<DataSelectDto>> GetAllSelect();
        Task<Personcs> GetByName(string name);
    }
}
