using AddressBook.API.DTO;
using AddressBook.Application.Contracts.Persistence;
using AddressBook.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AddressBook.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly IPersonRepository _personRepository;
        private readonly IAddressRepository _addressRepository;

        public AddressesController(IPersonRepository personRepository, IAddressRepository addressRepository)
        {
            _personRepository = personRepository;
            _addressRepository = addressRepository;
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> GetAddress([FromRoute] long id)
        {
            var address = await _addressRepository.GetByIdAsync(id);
            if (address is null)
            {
                return NotFound("Address not found");
            }
            return Ok(address);
        }

        [HttpGet]
        [Route("People/{idPerson}")]
        public async Task<ActionResult> GetByPerson([FromRoute] long idPerson)
        {
            var address = await _addressRepository.GetAync(e => e.PersonId == idPerson);
            if (address is null)
            {
                return NotFound("Address not found");
            }
            return Ok(address);
        }

        [HttpPost]
        public async Task<ActionResult> PostAddress([FromBody] AddressDTO dto)
        {
            var person = await _personRepository.GetByIdAsync(dto.PersonId);
            if (person is null)
            {
                return NotFound("Person not found");
            }
            var addressToCreate = new Address()
            {
                HomeAddress = dto.HomeAddress,
                Person = person,
            };
            await _addressRepository.AddAsync(addressToCreate);
            return Ok(addressToCreate);
        }

        [HttpGet]
        public async Task<ActionResult> GetAllAddress()
        {
            return Ok(await _addressRepository.GetAllAync());
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult> PutAddress([FromRoute] long id, [FromBody] AddressDTO dto)
        {
            var person = await _personRepository.GetByIdAsync(dto.PersonId);
            if (person is null)
            {
                return NotFound("Person not found");
            }
            var addressToUpdate = await _addressRepository.GetByIdAsync(id);
            if (addressToUpdate is null)
            {
                return NotFound("Address not found");
            }
            addressToUpdate.HomeAddress = dto.HomeAddress;
            addressToUpdate.Person = person;
            addressToUpdate.LastModifiedDate = DateTime.Now;
            await _addressRepository.UpdateAsync(addressToUpdate);
            return Ok(addressToUpdate);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeleteAddress([FromRoute] long id)
        {
            var addressToDelete = await _addressRepository.GetByIdAsync(id);
            if (addressToDelete is null)
            {
                return NotFound("Address not found");
            }
            await _addressRepository.DeleteAsync(addressToDelete);
            return Ok(addressToDelete);
        }
    }
}
