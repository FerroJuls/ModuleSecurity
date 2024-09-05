using Business.Interface;
using Data.Interfaces;
using Entity.DTO;
using Entity.Model.Security;
using System.Diagnostics.Metrics;

namespace Business.Implements
{
    public class CountryBusiness : ICountryBusiness
    {
        protected readonly ICountryData data;

        public CountryBusiness(ICountryData data)
        {
            this.data = data;
        }

        public async Task Delete(int id)
        {
            await this.data.Delete(id);
        }

        public async Task<IEnumerable<CountryDto>> GetAll()
        {
            IEnumerable<Country> countries = await this.data.GetAll();
            var countryDtos = countries.Select(country => new CountryDto
            {
                Id = country.Id,
                Name = country.Name,
                IsoCode = country.IsoCode,
            });

            return countryDtos;
        }

        public async Task<CountryDto> GetById(int id)
        {
            Country country = await this.data.GetById(id);
            CountryDto countryDto = new CountryDto
            {
                Id = country.Id,
                Name = country.Name,
            };
            return countryDto;
        }

        public Country mapearDatos(Country country, CountryDto entity)
        {
            country.Id = entity.Id;
            country.Name = entity.Name;
            return country;
        }

        public async Task<Country> Save(CountryDto entity)
        {
            Country country = new Country
            {
                CreateAt = DateTime.Now.AddHours(-5)
            };
            country = this.mapearDatos(country, entity);

            return await this.data.Save(country);
        }

        public async Task Update(CountryDto entity)
        {
            Country country = await this.data.GetById(entity.Id);
            if (country == null)
            {
                throw new Exception("Registro no encontrado");
            }

            country = this.mapearDatos(country, entity);
            await this.data.Update(country);
        }
    }
}
