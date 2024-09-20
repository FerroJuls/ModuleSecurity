using Data.Interfaces;
using Entity.Context;
using Entity.DTO;
using Entity.Model.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Data.Implements
{
    public class StateData : IStateData
    {
        private readonly ApplicationDBContext context;
        protected readonly IConfiguration configuration;

        public StateData(ApplicationDBContext context, IConfiguration configuration)
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
            context.States.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task<State> GetById(int id)
        {
            try
            {
                return await context.States
                    .Include(s => s.Countries) 
                    .FirstOrDefaultAsync(s => s.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el State por Id", ex);
            }
        }


        public async Task<State> Save(State entity)
        {
            if (entity.Countries != null)
            {
                var existingCountry = await context.Countries.FindAsync(entity.Countries.Id);
                if (existingCountry != null)
                {
                    entity.Countries = existingCountry;
                }
            }

            context.States.Add(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task Update(State entity)
        {
            if (entity.Countries != null)
            {
                var existingCountry = await context.Countries.FindAsync(entity.Countries.Id);

                if (existingCountry != null)
                {
                    context.Entry(existingCountry).State = EntityState.Unchanged;
                    entity.Countries = existingCountry;
                }
            }

            context.Entry(entity).State = EntityState.Modified;

            await context.SaveChangesAsync();
        }


        public async Task<State> GetByName(string name)
        {
            return await this.context.States.AsNoTracking().Where(item => item.Name == name).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<DataSelectDto>> GetAllSelect()
        {
            try
            {
                var sql = @"
                    SELECT Id, CONCAT(Name) AS TextoMostrar
                    FROM State
                    WHERE Deleted_at IS NULL AND State = 1
                    ORDER BY Id ASC";

                return await this.context.QueryAsync<DataSelectDto>(sql);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la lista de selección de States", ex);
            }
        }

        public async Task<IEnumerable<State>> GetAll()
        {
            try
            {
                return await context.States
                    .Where(s => s.state == true)
                    .OrderBy(s => s.Id)
                    .Include(s => s.Countries) 
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener todos los States", ex);
            }
        }

    }
}
