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
            context.Persons.Update(entity);
            await context.SaveChangesAsync();
        }

        public async Task<Person> GetById(int id)
        {
            try
            {
                var sql = @"SELECT * FROM Person WHERE Id = @Id ORDER BY Id ASC";
                return await this.context.QueryFirstOrDefaultAsync<Person>(sql, new { Id = id });

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Person> Save(Person entity)
        {
            context.Persons.Add(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task Update(Person entity)
        {
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
                    SELECT Id, CONCAT(Name, ' - ', Description) AS TextoMostrar
                    FROM Person
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
                var sql = "SELECT * FROM Person ORDER BY Id ASC";
                return await this.context.QueryAsync<Person>(sql);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener todos los Persons", ex);
            }
        }

    }
}
