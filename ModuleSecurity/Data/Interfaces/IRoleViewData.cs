using Entity.Model.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.interfaces
{
    public interface IRoleViewData
    {
        Task Delete(int id);
        Task<RoleView> GetById(int id);
        Task<RoleView> Save(RoleView entity);
        Task Update(RoleView entity);
        Task<IEnumerable<RoleView>> GetAll();

        //Task<IEnumerable<DataSelectDto>> GetAllSelect();

        //Task<RoleView> GetByName(string name);
    }
}
