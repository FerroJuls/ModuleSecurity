using Business.Interface;
using Data.Interfaces;
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
            IEnumerable<UserRole> UserRoles = await this.data.GetAll();
            var UserRoleDtos = UserRoles.Select(view => new UserRoleDto
            {
                Id = view.Id
            });

            return UserRoleDtos;
        }

        //public async Task<IEnumerable<DataSelectDto>> GetAllSelect()
        //{
        //    return await this.data.GetAllSelect();
        //}

        public async Task<UserRoleDto> GetById(int id)
        {
            UserRole userRole = await this.data.GetById(id);
            UserRoleDto userRoleDto = new UserRoleDto();

            userRoleDto.Id = userRoleDto.Id;

            return userRoleDto;
        }

        public UserRole mapearDatos(UserRole userRole, UserRoleDto entity)
        {
            userRole.Id = entity.Id;

            return userRole;
        }

        public async Task<UserRole> Save(UserRoleDto entity)
        {
            UserRole userRole = new UserRole();
            userRole.CreateAt = DateTime.Now.AddHours(-5);
            userRole = this.mapearDatos(userRole, entity);
            userRole.Role = null;
            userRole.User = null;

            return await this.data.Save(userRole);
        }

        public async Task Update(UserRoleDto entity)
        {
            UserRole userRole = await this.data.GetById(entity.Id);
            if (userRole == null)
            {
                throw new Exception("Registro no encontrado");
            }

            userRole = this.mapearDatos(userRole, entity);
            await this.data.Update(userRole);
        }
    }
}
