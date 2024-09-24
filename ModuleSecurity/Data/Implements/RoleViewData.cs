using Data.Interfaces;
using Entity.Context;
using Entity.DTO;
using Entity.Model.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Data.Implements
{
    public class RoleViewData : IRoleViewData
    {
        private readonly ApplicationDBContext context;
        protected readonly IConfiguration configuration;

        public RoleViewData(ApplicationDBContext context, IConfiguration configuration)
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
            context.RoleViews.Remove(entity);
            await context.SaveChangesAsync();
        }


        public async Task<RoleView> GetById(int id)
        {
            try
            {
                return await context.RoleViews
                    .Include(rv => rv.Role)
                    .Include(rv => rv.View)
                    .FirstOrDefaultAsync(rv => rv.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el UserRole por Id", ex);
            }
        }


        public async Task<RoleView> Save(RoleView entity)
        {
            if (entity.View != null)
            {
                var existingView = await context.Views.FindAsync(entity.View.Id);
                if (existingView != null)
                {
                    entity.View = existingView;
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

            context.RoleViews.Add(entity);
            await context.SaveChangesAsync();
            return entity;
        }


        public async Task Update(RoleView entity)
        {
            if (entity.View != null)
            {
                var existingView = await context.Views.FindAsync(entity.View.Id);

                if (existingView != null)
                {
                    context.Entry(existingView).State = EntityState.Unchanged;
                    entity.View = existingView;
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

        public async Task<RoleView> GetByName(int id)
        {
            return await this.context.RoleViews.AsNoTracking().Where(item => item.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<RoleView>> GetAll()
        {
            try
            {
                return await context.RoleViews
                    .Where(s => s.State == true)
                    .Include(rv => rv.View)
                    .Include(rv => rv.Role)
                    .OrderBy(rv => rv.Id)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener todos los UserRoles", ex);
            }
        }

    }
}
