using AddressBook.API.DTO;
using AddressBook.Application.Contracts.Persistence;
using AddressBook.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AddressBook.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhonesController : ControllerBase
    {
        private readonly IPersonRepository _personRepository;
        private readonly IPhoneRepository _phoneRepository;

        public PhonesController(IPersonRepository personRepository, IPhoneRepository phoneRepository)
        {
            _personRepository = personRepository;
            _phoneRepository = phoneRepository;
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> GetPhone([FromRoute] long id)
        {
            var Phone = await _phoneRepository.GetByIdAsync(id);
            if (Phone is null)
            {
                return NotFound("Phone not found");
            }
            return Ok(Phone);
        }

        [HttpGet]
        [Route("People/{idPerson}")]
        public async Task<ActionResult> GetByPerson([FromRoute] long idPerson)
        {
            var phone = await _phoneRepository.GetAync(e => e.PersonId == idPerson);
            if (phone is null)
            {
                return NotFound("Phone not found");
            }
            return Ok(phone);
        }

        [HttpPost]
        public async Task<ActionResult> PostPhone([FromBody] PhoneDTO dto)
        {
            var person = await _personRepository.GetByIdAsync(dto.PersonId);
            if (person is null)
            {
                return NotFound("Person not found");
            }
            var phoneToCreate = new Phone()
            {
                PhoneNumber = dto.PhoneNumber,
                Person = person,
            };
            await _phoneRepository.AddAsync(phoneToCreate);
            return Ok(phoneToCreate);
        }

        [HttpGet]
        public async Task<ActionResult> GetAllPhone()
        {
            return Ok(await _phoneRepository.GetAllAync());
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult> PutPhone([FromRoute] long id, [FromBody] PhoneDTO dto)
        {
            var person = await _personRepository.GetByIdAsync(dto.PersonId);
            if (person is null)
            {
                return NotFound("Person not found");
            }
            var phoneToUpdate = await _phoneRepository.GetByIdAsync(id);
            if (phoneToUpdate is null)
            {
                return NotFound("Phone not found");
            }
            phoneToUpdate.PhoneNumber = dto.PhoneNumber;
            phoneToUpdate.Person = person;
            phoneToUpdate.LastModifiedDate = DateTime.Now;
            await _phoneRepository.UpdateAsync(phoneToUpdate);
            return Ok(phoneToUpdate);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeletePhone([FromRoute] long id)
        {
            var phoneToDelete = await _phoneRepository.GetByIdAsync(id);
            if (phoneToDelete is null)
            {
                return NotFound("Phone not found");
            }
            await _phoneRepository.DeleteAsync(phoneToDelete);
            return Ok(phoneToDelete);
        }
    }
}
