using Data.Interfaces;
using Entity.Context;
using Entity.DTO;
using Entity.Model.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Diagnostics.Metrics;

namespace Data.Implements
{
    public class CountryData : ICountryData
    {
        private readonly ApplicationDBContext context;
        protected readonly IConfiguration configuration;

        public CountryData(ApplicationDBContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

        public async Task Delete(int id)
        {
            var entity = await GetById(id);
            if (entity == null)
                throw new Exception("Registro no encontrado");

            entity.DeleteAt = DateTime.Parse(DateTime.Today.ToString());
            context.Countries.Update(entity);
            await context.SaveChangesAsync();
        }

        public async Task<Country> GetById(int id)
        {
            var sql = @"SELECT * FROM Country WHERE Id = @Id ORDER BY Id ASC";
            return await this.context.QueryFirstOrDefaultAsync<Country>(sql, new { Id = id });
        }

        public async Task<Country> Save(Country entity)
        {
            context.Countries.Add(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task Update(Country entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task<Country> GetByName(string name)
        {
            return await this.context.Countries.AsNoTracking().Where(item => item.Name == name).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<DataSelectDto>> GetAllSelect()
        {
            try
            {
                var sql = @"
                    SELECT Id, CONCAT(Name) AS TextoMostrar
                    FROM Country
                    WHERE Deleted_at IS NULL AND State = 1
                    ORDER BY Id ASC";

                return await this.context.QueryAsync<DataSelectDto>(sql);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la lista de selección de Countries", ex);
            }
        }

        public async Task<IEnumerable<Country>> GetAll()
        {
            try
            {
                var sql = "SELECT * FROM Country ORDER BY Id ASC";
                return await this.context.QueryAsync<Country>(sql);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener todos los Countries", ex);
            }
        }
    }
}
