using AddressBook.API.DTO;
using AddressBook.Application.Contracts.Persistence;
using AddressBook.Domain;
using AddressBook.Infrastructure.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AddressBook.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly IPersonRepository _personRepository;

        public PeopleController(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> GetPerson([FromRoute] long id)
        {
            var person = await _personRepository.GetIncludedByIdAsync(id);
            if (person is null)
            {
                return NotFound("Person not found");
            }
            return Ok(person);
        }
        /*
       
        [HttpGet]
        public async Task<IActionResult> GetAsync(long id)
        {
            Person book = null;
            try
            {
                //  await method - Asynchronously 
                book = await _personRepository.GetAync(c=>c.Id==id);
                if (book == null)
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            // do more stuff

            return Ok(book);
        }
         */
        [HttpPost]
        public async Task<ActionResult<Person>> PostPeople([FromBody] PersonDTO dto)
        {
            var PersonToCreate = new Person()
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
            };
            await _personRepository.AddAsync(PersonToCreate);
            return Ok(PersonToCreate);
        }

        [HttpGet]
        public async Task<ActionResult<List<Person>>> GetAllPeople()
        {
          return Ok(await _personRepository.GetAllOrderedIncludedByDateAsync());
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult> PutPeople([FromRoute] long id, [FromBody] PersonDTO dto)
        {
            var personToUpdate = await _personRepository.GetByIdAsync(id);
            if (personToUpdate is null)
            {
                return NotFound("Person not found");
            }
            personToUpdate.FirstName = dto.FirstName;
            personToUpdate.LastName = dto.LastName;
            personToUpdate.LastModifiedDate = DateTime.Now;
            await _personRepository.UpdateAsync(personToUpdate);
            return Ok(personToUpdate);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeletePerson([FromRoute] long id)
        {
            var personToDelete = await _personRepository.GetByIdAsync(id);
            if (personToDelete is null)
            {
                return NotFound("Person not found");
            }
            await _personRepository.DeleteAsync(personToDelete);
            return Ok(personToDelete);
        }
    }
}
