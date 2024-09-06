using Business.Interfaces;
using Data.interfaces;
using Entity.DTO;
using Entity.Model.Security;

namespace Business.Implements
{
    public class RoleViewBusiness : IRoleViewBusiness
    {
        protected readonly IRoleViewData data;

        public RoleViewBusiness(IRoleViewData data)
        {
            this.data = data;
        }

        public async Task Delete(int id)
        {
            await this.data.Delete(id);
        }

        public async Task<IEnumerable<RoleViewDto>> GetAll()
        {
            IEnumerable<RoleView> roleview = await this.data.GetAll();
            var roleviewDtos = roleview.Select(roleview => new RoleViewDto
            {
                Id = roleview.Id,
            });

            return roleviewDtos;
        }

        //public async Task<IEnumerable<DataSelectDto>> GetAllSelect()
        //{
        //    return await this.data.GetAllSelect();
        //}

        public async Task<RoleViewDto> GetById(int id)
        {
            RoleView roleview = await this.data.GetById(id);
            RoleViewDto roleviewDto = new RoleViewDto();

            roleviewDto.Id = roleview.Id;
         
            return roleviewDto;
        }

        public RoleView mapearDatos(RoleView roleview, RoleViewDto entity)
        {
            roleview.Id = entity.Id;
        
            return roleview;
        }

        public async Task<RoleView> Save(RoleViewDto entity)
        {
            RoleView roleview = new RoleView();
            roleview.CreateAt = DateTime.Now.AddHours(-5);
            roleview = this.mapearDatos(roleview, entity);
 
            return await this.data.Save(roleview);
        }

        public async Task Update(RoleViewDto entity)
        {
            RoleView roleview = await this.data.GetById(entity.Id);
            if (roleview == null)
            {
                throw new Exception("Registro no encontrado");
            }

            roleview = this.mapearDatos(roleview, entity);
            await this.data.Update(roleview);
        }
    }
}