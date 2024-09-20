using Data.Interfaces;
using Entity.Context;
using Entity.DTO;
using Entity.Model.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Data.Implements
{
    public class UserData : IUserData
    {
        private readonly ApplicationDBContext context;
        protected readonly IConfiguration configuration;

        public UserData(ApplicationDBContext context, IConfiguration configuration)
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
            context.Users.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task<User> GetById(int id)
        {
            try
            {
                return await context.Users
                    .Include(u => u.Person)
                    .FirstOrDefaultAsync(u => u.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el State por Id", ex);
            }
        }

        public async Task<User> Save(User entity)
        {
            if (entity.Person != null)
            {
                var existingPerson = await context.Persons.FindAsync(entity.Person.Id);
                if (existingPerson != null)
                {
                    entity.Person = existingPerson;
                }
            }

            context.Users.Add(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task Update(User entity)
        {
            if (entity.Person != null)
            {
                var existingPerson = await context.Persons.FindAsync(entity.Person.Id);

                if (existingPerson != null)
                {
                    context.Entry(existingPerson).State = EntityState.Unchanged;
                    entity.Person = existingPerson;
                }
            }

            context.Entry(entity).State = EntityState.Modified;

            await context.SaveChangesAsync();
        }

        public async Task<User> GetByUsername(string username)
        {
            return await this.context.Users.AsNoTracking().Where(item => item.Username == username).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<DataSelectDto>> GetAllSelect()
        {
            try
            {
                var sql = @"
                    SELECT Id, CONCAT(Name, ' - ', Description) AS TextoMostrar
                    FROM User
                    WHERE Deleted_at IS NULL AND State = 1
                    ORDER BY Id ASC";

                return await this.context.QueryAsync<DataSelectDto>(sql);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la lista de selección de Users", ex);
            }
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            try
            {
                return await context.Users
                    .Where(u => u.State == true)
                    .OrderBy(u => u.Id)
                    .Include(u => u.Person)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener todos las personas", ex);
            }
        }

        public Task<User> GetByName(string username)
        {
            throw new NotImplementedException();
        }
    }
}
