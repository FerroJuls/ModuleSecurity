using Data.Interfaces;
using Entity.Context;
using Entity.DTO;
using Entity.Model.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Data.Implements
{
    public class PersonData : IPersonData
    {
        private readonly ApplicationDBContext context;
        protected readonly IConfiguration configuration;

        public PersonData(ApplicationDBContext context, IConfiguration configuration)
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
            context.Persons.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task<Person> GetById(int id)
        {
            try
            {
                return await context.Persons
                    .Include(p => p.City)
                    .FirstOrDefaultAsync(p => p.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el State por Id", ex);
            }
        }

        public async Task<Person> Save(Person entity)
        {
            if (entity.City != null)
            {
                var existingCity = await context.Cities.FindAsync(entity.City.Id);
                if (existingCity != null)
                {
                    entity.City = existingCity;
                }
            }

            context.Persons.Add(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task Update(Person entity)
        {
            if (entity.City != null)
            {
                var existingCity = await context.Cities.FindAsync(entity.City.Id);

                if (existingCity != null)
                {
                    context.Entry(existingCity).State = EntityState.Unchanged;
                    entity.City = existingCity;
                }
            }

            context.Entry(entity).State = EntityState.Modified;

            await context.SaveChangesAsync();
        }

        public async Task<Person> GetByName(string first_name)
        {
            return await this.context.Persons.AsNoTracking().Where(item => item.First_name == first_name).FirstOrDefaultAsync();
        }

        //


        public async Task<IEnumerable<DataSelectDto>> GetAllSelect()
        {
            try
            {
                var sql = @"
                    SELECT Id, CONCAT(Name, ' - ', First_name) AS TextoMostrar
                    FROM Persons
                    WHERE Deleted_at IS NULL AND State = 1
                    ORDER BY Id ASC";

                return await this.context.QueryAsync<DataSelectDto>(sql);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la lista de selección de Persons", ex);
            }
        }

        public async Task<IEnumerable<Person>> GetAll()
        {
            try
            {
                return await context.Persons
                    .Where(p => p.State == true)
                    .OrderBy(p => p.Id)
                    .Include(p => p.City)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener todos las personas", ex);
            }
        }

    }
}
