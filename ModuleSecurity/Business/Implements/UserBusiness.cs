using Business.Interfaces;
using Data.interfaces;
using Entity.DTO;
using Entity.Model.Security;



namespace Business.Implements
{
    public class UserBusiness : IUserBusiness
    {
        protected readonly IUserData data;

        public UserBusiness(IUserData data)
        {
            this.data = data;
        }

        public async Task Delete(int id)
        {
            await this.data.Delete(id);
        }

        public async Task<IEnumerable<UserDto>> GetAll()
        {
            IEnumerable<User> user = await this.data.GetAll();
            var userDtos = user.Select(user => new UserDto
            {
                Id = user.Id,
                Username = user.Username,
   
            });

            return userDtos;
        }

        //public async Task<IEnumerable<DataSelectDto>> GetAllSelect()
        //{
        //    return await this.data.GetAllSelect();
        //}

        public async Task<UserDto> GetById(int id)
        {
            User user = await this.data.GetById(id);
            UserDto userDto = new UserDto();

            userDto.Id = user.Id;
            userDto.Username = user.Username;
         
            return userDto;
        }

        public Task<User> GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public User mapearDatos(User user, UserDto entity)
        {
            user.Id = entity.Id;
            user.Username = entity.Username;
      
            return user;
        }

        public async Task<User> Save(UserDto entity)
        {
            User user = new User();
            user.CreateAt = DateTime.Now.AddHours(-5);
            user = this.mapearDatos(user, entity);
            user.Personcs = null;

            return await this.data.Save(user);
        }

        public async Task Update(UserDto entity)
        {
            User user = await this.data.GetById(entity.Id);
            if (user == null)
            {
                throw new Exception("Registro no encontrado");
            }

            user = this.mapearDatos(user, entity);
            await this.data.Update(user);
        }
    }
}