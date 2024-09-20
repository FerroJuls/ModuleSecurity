
using Data.Interfaces;
using Entity.Context;
using Entity.DTO;
using Entity.Model.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Data.Implements
{
    public class CityData : ICityData
    {
        private readonly ApplicationDBContext context;
        protected readonly IConfiguration configuration;

        public CityData(ApplicationDBContext context, IConfiguration configuration)
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
            context.Cities.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task<City> GetById(int id)
        {
            try
            {
                return await context.Cities
                    .Include(c => c.state)
                    .FirstOrDefaultAsync(c => c.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el State por Id", ex);
            }
        }

        public async Task<City> Save(City entity)
        {
            if (entity.state != null)
            {
                var existingState = await context.States.FindAsync(entity.state.Id);
                if (existingState != null)
                {
                    entity.state = existingState;
                }
            }

            context.Cities.Add(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task Update(City entity)
        {
            if (entity.state != null)
            {
                var existingState = await context.States.FindAsync(entity.state.Id);

                if (existingState != null)
                {
                    context.Entry(existingState).State = EntityState.Unchanged;
                    entity.state = existingState;
                }
            }

            context.Entry(entity).State = EntityState.Modified;

            await context.SaveChangesAsync();
        }

        public async Task<City> GetByName(string name)
        {
            return await this.context.Cities.AsNoTracking().Where(item => item.Name == name).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<DataSelectDto>> GetAllSelect()
        {
            try
            {
                var sql = @"
                    SELECT Id, CONCAT(Name) AS TextoMostrar
                    FROM Cities
                    WHERE Deleted_at IS NULL AND State = 1
                    ORDER BY Id ASC";

                return await this.context.QueryAsync<DataSelectDto>(sql);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la lista de selección de Cities", ex);
            }
        }

        public async Task<IEnumerable<City>> GetAll()
        {
            try
            {
                return await context.Cities
                    .Where(c => c.Estado == true)
                    .OrderBy(c => c.Id)
                    .Include(c => c.state)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener todos los Cities", ex);
            }
        }

    }
}
