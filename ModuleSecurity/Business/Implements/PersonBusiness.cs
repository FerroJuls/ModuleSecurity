using Business.Interfaces;
using Data.interfaces;
using Entity.DTO;
using Entity.Model.Security;

namespace Business.Implements
{
    public class PersonBusiness :  IPersonBusiness
    {
        protected readonly IPersonData data;

        public PersonBusiness(IPersonData data)
        {
            this.data = data;
        }

        public async Task Delete(int id)
        {
            await this.data.Delete(id);
        }

        public async Task<IEnumerable<PersonDto>> GetAll()
        {
            IEnumerable<Personcs> person = await this.data.GetAll();
            var personDtos = person.Select(person => new PersonDto
            {
                Id = person.Id,
                First_name = person.First_name,
                Email = person.Email,
                Type_document = person.Type_document,
            });

            return personDtos;
        }

        //public async Task<IEnumerable<DataSelectDto>> GetAllSelect()
        //{
        //    return await this.data.GetAllSelect();
        //}

        public async Task<PersonDto> GetById(int id)
        {
            Personcs person = await this.data.GetById(id);
            PersonDto personDto = new PersonDto();

            personDto.Id = person.Id;
            personDto.First_name = person.First_name;
            personDto.Email = person.Email;
            personDto.Type_document = person.Type_document;

            return personDto;
        }

        public Task<Personcs> GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public Personcs mapearDatos(Personcs person, PersonDto entity)
        {
            person.Id = entity.Id;
            person.First_name = entity.First_name;
            person.Email = entity.Email;
            person.Type_document = entity.Type_document;

            return person;
        }

        public async Task<Personcs> Save(PersonDto entity)
        {
            Personcs person = new Personcs();
            person.CreateAt = DateTime.Now.AddHours(-5);
            person = this.mapearDatos(person, entity);

            return await this.data.Save(person);
        }

        public async Task Update(PersonDto entity)
        {
            Personcs person = await this.data.GetById(entity.Id);
            if (person == null)
            {
                throw new Exception("Registro no encontrado");
            }

            person = this.mapearDatos(person, entity);
            await this.data.Update(person);
        }
    }
}