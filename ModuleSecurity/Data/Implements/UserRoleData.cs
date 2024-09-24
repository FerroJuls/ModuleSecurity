using Data.Interfaces;
using Entity.Context;
using Entity.DTO;
using Entity.Model.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Data.Implements
{
    public class UserRoleData : IUserRoleData
    {
        private readonly ApplicationDBContext context;
        protected readonly IConfiguration configuration;

        public UserRoleData(ApplicationDBContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

        public async Task Delete(int id)
        {
            var entity = await GetById(id);
            if (entity == null)
            {
                throw new Exception("Registro no encontrado");
            }
            context.UserRoles.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task<UserRole> GetById(int id)
        {
            try
            {
                return await context.UserRoles
                    .Include(ur => ur.Role)   
                    .Include(ur => ur.User)  
                    .FirstOrDefaultAsync(ur => ur.Id == id);  
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el UserRole por Id", ex);
            }
        }


        public async Task<UserRole> Save(UserRole entity)
        {
            if (entity.User != null)
            {
                var existingUser = await context.Users.FindAsync(entity.User.Id);
                if (existingUser != null)
                {
                    entity.User = existingUser;
                }
            }

            if (entity.Role != null)
            {
                var existingRole = await context.Roles.FindAsync(entity.Role.Id);
                if (existingRole != null)
                {
                    entity.Role = existingRole;
                }
            }

            context.UserRoles.Add(entity);
            await context.SaveChangesAsync();
            return entity;
        }


        public async Task Update(UserRole entity)
        {
            if (entity.User != null)
            {
                var existingUser = await context.Users.FindAsync(entity.User.Id);

                if (existingUser != null)
                {
                    context.Entry(existingUser).State = EntityState.Unchanged;
                    entity.User = existingUser;
                }
            }

            if (entity.Role != null)
            {
                var existingRole = await context.Roles.FindAsync(entity.Role.Id);

                if (existingRole != null)
                {
                    context.Entry(existingRole).State = EntityState.Unchanged;
                    entity.Role = existingRole;
                }
            }

            context.Entry(entity).State = EntityState.Modified;

            await context.SaveChangesAsync();
        }


        public async Task<UserRole> GetByName(int id)
        {
            return await this.context.UserRoles.AsNoTracking().Where(item => item.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<UserRole>> GetAll()
        {
            try
            {
                return await context.UserRoles
                    .Where(s => s.State == true)
                    .Include(ur => ur.User) 
                    .Include(ur => ur.Role) 
                    .OrderBy(ur => ur.Id) 
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener todos los UserRoles", ex);
            }
        }

    }
}
