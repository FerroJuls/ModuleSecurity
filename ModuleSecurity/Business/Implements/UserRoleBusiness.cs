using Business.Interfaces;
using Data.interfaces;
using Entity.DTO;
using Entity.Model.Security;

namespace Business.Implements
{
    public class UserRoleBusiness : IUserRoleBusiness
    {
        protected readonly IUserRoleData data;

        public UserRoleBusiness(IUserRoleData data)
        {
            this.data = data;
        }

        public async Task Delete(int id)
        {
            await this.data.Delete(id);
        }

        public async Task<IEnumerable<UserRoleDto>> GetAll()
        {
            IEnumerable<UserRole> userrole = await this.data.GetAll();
            var userroleDtos = userrole.Select(userrole => new UserRoleDto
            {
                Id = userrole.Id,
 
            });

            return userroleDtos;
        }

        //public async Task<IEnumerable<DataSelectDto>> GetAllSelect()
        //{
        //    return await this.data.GetAllSelect();
        //}

        public async Task<UserRoleDto> GetById(int id)
        {
            UserRole userrole = await this.data.GetById(id);
            UserRoleDto userroleDto = new UserRoleDto();

            userroleDto.Id = userrole.Id;
         
            return userroleDto;
        }

        public UserRole mapearDatos(UserRole userrole, UserRoleDto entity)
        {
            userrole.Id = entity.Id;
         
            return userrole;
        }

        public async Task<UserRole> Save(UserRoleDto entity)
        {
            UserRole userrole = new UserRole();
            userrole.CreateAt = DateTime.Now.AddHours(-5);
            userrole = this.mapearDatos(userrole, entity);

            return await this.data.Save(userrole);
        }

        public async Task Update(UserRoleDto entity)
        {
            UserRole userrole = await this.data.GetById(entity.Id);
            if (userrole == null)
            {
                throw new Exception("Registro no encontrado");
            }

            userrole = this.mapearDatos(userrole, entity);
            await this.data.Update(userrole);
        }
    }
}
