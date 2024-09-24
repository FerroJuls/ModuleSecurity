using Data.Interfaces;
using Entity.Context;
using Entity.DTO;
using Entity.Model.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Data.Implements
{
    public class ViewData : IViewData
    {
        private readonly ApplicationDBContext context;
        protected readonly IConfiguration configuration;

        public ViewData(ApplicationDBContext context, IConfiguration configuration)
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
            };
            context.Views.Update(entity);
            await context.SaveChangesAsync();
        }

        public async Task<View> GetById(int id)
        {
            try
            {
                return await context.Views
                    .Include(v => v.Module)
                    .FirstOrDefaultAsync(v => v.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el View por Id", ex);
            }
        }

        public async Task<View> Save(View entity)
        {
            if (entity.Module != null)
            {
                var existingModule = await context.Modules.FindAsync(entity.Module.Id);
                if (existingModule != null)
                {
                    entity.Module = existingModule;
                }
            }
            context.Views.Add(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task Update(View entity)
        {
            if (entity.Module != null)
            {
                var existingModule = await context.Modules.FindAsync(entity.Module.Id);

                if (existingModule != null)
                {
                    context.Entry(existingModule).State = EntityState.Unchanged;
                    entity.Module = existingModule;
                }
            }
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task<View> GetByName(string name)
        {
            return await this.context.Views.AsNoTracking().Where(item => item.Name == name).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<DataSelectDto>> GetAllSelect()
        {
            try
            {
                var sql = @"
                    SELECT Id, CONCAT(Name, ' - ', Description) AS TextoMostrar
                    FROM Views
                    WHERE Deleted_at IS NULL AND State = 1
                    ORDER BY Id ASC";

                return await this.context.QueryAsync<DataSelectDto>(sql);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la lista de selección de Views", ex);
            }
        }

        public async Task<IEnumerable<View>> GetAll()
        {
            try
            {
                return await context.Views
                    .Where(v => v.State == true)
                    .OrderBy(v => v.Id)
                    .Include(v => v.Module)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener todos los Views", ex);
            }
        }
    }
}
