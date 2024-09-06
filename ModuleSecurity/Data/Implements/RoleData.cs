using Data.Interfaces;
using Entity.Context;
using Entity.DTO;
using Entity.Model.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Data.Implements
{
    public class RoleData : IRoleData
    {
        private readonly ApplicationDBContext context;
        protected readonly IConfiguration configuration;

        public RoleData(ApplicationDBContext context, IConfiguration configuration)
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
            context.Roles.Update(entity);
            await context.SaveChangesAsync();
        }


        public async Task<Role> GetById(int id)
        {
            try
            {
                var sql = @"SELECT * FROM Roles WHERE Id = @Id ORDER BY Id ASC";
                return await this.context.QueryFirstOrDefaultAsync<Role>(sql, new { Id = id });

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Role> Save(Role entity)
        {
            context.Roles.Add(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task Update(Role entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task<Role> GetByName(string name)
        {
            return await this.context.Roles.AsNoTracking().Where(item => item.Name == name).FirstOrDefaultAsync();
        }

        //public async Task<IEnumerable<DataSelectDto>> GetAllSelect()
        //{
        //    var sql = @"
        //                SELECT Id, CONCAT(Name, ' - ', Description) AS TextoMostrar
        //                FROM Role
        //                WHERE Deleted_at IS NULL AND State = 1
        //                ORDER BY Id ASC";
        //    return await this.context.QueryAsync<DataSelectDto>(sql);
        //}

        public async Task<IEnumerable<DataSelectDto>> GetAllSelect()
        {
            try
            {
                var sql = @"
                    SELECT Id, CONCAT(Name, ' - ', Description) AS TextoMostrar
                    FROM Roles
                    WHERE Deleted_at IS NULL AND State = 1
                    ORDER BY Id ASC";

                return await this.context.QueryAsync<DataSelectDto>(sql);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la lista de selección de Roles", ex);
            }
        }

        //public async Task<IEnumerable<Role>> GetAll()
        //{
        //    var sql = @"SELECT * FROM Role ORDER BY Id ASC";
        //    return await this.context.QueryAsync<Role>(sql);
        //}


        public async Task<IEnumerable<Role>> GetAll()
        {
            try
            {
                var sql = "SELECT * FROM Roles ORDER BY Id ASC";
                return await this.context.QueryAsync<Role>(sql);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener todos los Roles", ex);
            }
        }



    }
}
