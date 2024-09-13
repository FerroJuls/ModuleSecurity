using Business.Interface;
using Data.Interfaces;
using Entity.DTO;
using Entity.Model.Security;

namespace Business.Implements
{
    public class ModuleBusiness : IModuleBusiness
    {
        protected readonly IModuleData data;

        public ModuleBusiness(IModuleData data)
        {
            this.data = data;
        }

        public async Task Delete(int id)
        {
            await this.data.Delete(id);
        }

        public async Task<IEnumerable<ModuleDto>> GetAll()
        {
            IEnumerable<Module> modules = await this.data.GetAll();
            var moduleDtos = modules.Select(module => new ModuleDto
            {
                Id = module.Id,
                Description = module.Description,
                State = module.State,
            });

            return moduleDtos;
        }


        public async Task<ModuleDto> GetById(int id)
        {
            Module module = await this.data.GetById(id);
            ModuleDto moduleDto = new ModuleDto();

            moduleDto.Id = module.Id;
            moduleDto.Description = module.Description;
            moduleDto.State = module.State;

            return moduleDto;
        }

        public Module mapearDatos(Module module, ModuleDto entity)
        {
            module.Id = entity.Id;
            module.Description = entity.Description;
            module.State = entity.State;

            return module;
        }

        public async Task<Module> Save(ModuleDto entity)
        {
            Module module = new Module();
            module.CreateAt = DateTime.Now.AddHours(-5);
            module = this.mapearDatos(module, entity);

            return await this.data.Save(module);
        }

        public async Task Update(ModuleDto entity)
        {
            Module module = await this.data.GetById(entity.Id);
            if (module == null)
            {
                throw new Exception("Registro no encontrado");
            }

            module = this.mapearDatos(module, entity);
            await this.data.Update(module);
        }
    }
}
