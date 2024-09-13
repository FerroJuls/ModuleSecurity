using Data.Interfaces;
using Entity.Context;
using Entity.DTO;
using Entity.Model.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace Data.Implements
{
    public class CountriesData : ICountriesData
    {
        private readonly ApplicationDBContext context;
        protected readonly IConfiguration configuration;

        public CountriesData(ApplicationDBContext context, IConfiguration configuration)
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
            context.Countries.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task<Countries> GetById(int id)
        {
            var sql = @"SELECT * FROM Countries WHERE Id = @Id ORDER BY Id ASC";
            return await this.context.QueryFirstOrDefaultAsync<Countries>(sql, new { Id = id });
        }

        public async Task<Countries> Save(Countries entity)
        {
            context.Countries.Add(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task Update(Countries entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task<Countries> GetByName(string name)
        {
            return await this.context.Countries.AsNoTracking().Where(item => item.Name == name).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<DataSelectDto>> GetAllSelect()
        {
            try
            {
                var sql = @"
                    SELECT Id, CONCAT(Name) AS TextoMostrar
                    FROM Countries
                    WHERE Deleted_at IS NULL AND State = 1
                    ORDER BY Id ASC";

                return await this.context.QueryAsync<DataSelectDto>(sql);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la lista de selección de Countries", ex);
            }
        }

        public async Task<IEnumerable<Countries>> GetAll()
        {
            try
            {
                var sql = "SELECT * FROM Countries WHERE State=true ORDER BY Id ASC";
                return await this.context.QueryAsync<Countries>(sql);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener todos los Countries", ex);
            }
        }
    }
}
