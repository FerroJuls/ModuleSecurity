using Entity.Model.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.interfaces
{
    public interface IPersonData
    {
        Task Delete(int id);
        Task<Personcs> GetById(int id);
        Task<Personcs> Save(Personcs entity);
        Task Update(Personcs entity);
        Task<IEnumerable<Personcs>> GetAll();

        //Task<IEnumerable<DataSelectDto>> GetAllSelect();

        Task<Personcs> GetByName(string name);
    }
}
