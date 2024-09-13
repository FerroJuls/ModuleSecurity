using Business.Interfaces;
using Data.Interfaces;
using Entity.DTO;
using Entity.Model.Security;
using System.Diagnostics.Metrics;

namespace Business.Implements
{
    public class CountriesBusiness : ICountriesBusiness
    {
        protected readonly ICountriesData data;

        public CountriesBusiness(ICountriesData data)
        {
            this.data = data;
        }

        public async Task Delete(int id)
        {
            await this.data.Delete(id);
        }

        public async Task<IEnumerable<CountriesDto>> GetAll()
        {
            IEnumerable<Countries> countries = await this.data.GetAll();
            var countriesDto = countries.Select(countries => new CountriesDto
            {
                Id = countries.Id,
                Name = countries.Name,
                State = countries.State,
            });

            return countriesDto;
        }

        public async Task<CountriesDto> GetById(int id)
        {
            Countries country = await this.data.GetById(id);
            CountriesDto countryDto = new CountriesDto
            {
                Id = country.Id,
                Name = country.Name,
                State = country.State,
            };
            return countryDto;
        }

        public Countries mapearDatos(Countries country, CountriesDto entity)
        {
            country.Id = entity.Id;
            country.Name = entity.Name;
            country.State = entity.State;
            return country;
        }

        public async Task<Countries> Save(CountriesDto entity)
        {
            Countries country = new Countries
            {
                CreateAt = DateTime.Now.AddHours(-5)
            };
            country = this.mapearDatos(country, entity);

            return await this.data.Save(country);
        }

        public async Task Update(CountriesDto entity)
        {
            Countries country = await this.data.GetById(entity.Id);
            if (country == null)
            {
                throw new Exception("Registro no encontrado");
            }

            country = this.mapearDatos(country, entity);
            await this.data.Update(country);
        }
    }
}
