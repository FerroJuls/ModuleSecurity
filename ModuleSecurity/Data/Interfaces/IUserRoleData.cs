using Entity.Model.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.interfaces
{
    public interface IUserRoleData
    {
        Task Delete(int id);
        Task<UserRole> GetById(int id);
        Task<UserRole> Save(UserRole entity);
        Task Update(UserRole entity);
        Task<IEnumerable<UserRole>> GetAll();

        //Task<IEnumerable<DataSelectDto>> GetAllSelect();

        //Task<UserRole> GetByName(string name);

    }
}
