﻿using Business.Interface;
using Data.Interfaces;
using Entity.DTO;
using Entity.Model.Security;

namespace Business.Implements
{
    public class ViewBusiness : IViewBusiness
    {
        protected readonly IViewData data;

        public ViewBusiness(IViewData data)
        {
            this.data = data;
        }

        public async Task Delete(int id)
        {
            await this.data.Delete(id);
        }

        public async Task<IEnumerable<ViewDto>> GetAll()
        {
            IEnumerable<View> views = await this.data.GetAll();
            var viewDtos = views.Select(view => new ViewDto
            {
                Id = view.Id,
                Name = view.Name,
                Description = view.Description,
                State = view.State,
                ModuleId = view.ModuleId,
                Module = view.Module.Description
            });

            return viewDtos;
        }

        public async Task<ViewDto> GetById(int id)
        {
            View view = await this.data.GetById(id);
            ViewDto viewDto = new ViewDto
            {
                Id = view.Id,
                Name = view.Name,
                Description = view.Description,
                State = view.State,
                ModuleId = view.ModuleId,
                Module = view.Module.Description
            };
            return viewDto;
        }

        public View mapearDatos(View view, ViewDto entity)
        {
            view.Id = entity.Id;
            view.Name = entity.Name;
            view.Description = entity.Description;
            view.State = entity.State;
            view.ModuleId = entity.ModuleId;
                
            return view;
        }

        public async Task<View> Save(ViewDto entity)
        {
            View view = new View
            {
                CreateAt = DateTime.Now.AddHours(-5)
            };
            view = this.mapearDatos(view, entity);
            return await this.data.Save(view);
        }

        public async Task Update(ViewDto entity)
        {
            View view = await this.data.GetById(entity.Id);
            if (view == null)
            {
                throw new Exception("Registro no encontrado");
            }

            view = this.mapearDatos(view, entity);
            await this.data.Update(view);
        }
    }
}

