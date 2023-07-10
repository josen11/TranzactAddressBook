using AddressBook.API.DTO;
using AddressBook.Application.Contracts.Persistence;
using AddressBook.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AddressBook.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailsController : ControllerBase
    {
        private readonly IPersonRepository _personRepository;
        private readonly IEmailRepository _emailRepository;

        public EmailsController(IPersonRepository personRepository, IEmailRepository emailRepository)
        {
            _personRepository = personRepository;
            _emailRepository = emailRepository;
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Email>> GetEmail([FromRoute] long id)
        {
            var email = await _emailRepository.GetByIdAsync(id);
            if (email is null)
            {
                return NotFound("Email not found");
            }
            return Ok(email);
        }

        [HttpGet]
        [Route("People/{idPerson}")]
        public async Task<ActionResult<Email>> GetByPerson([FromRoute] long idPerson)
        {
            var email = await _emailRepository.GetAync(e => e.PersonId == idPerson);
            if (email is null)
            {
                return NotFound("Email not found");
            }
            return Ok(email);
        }

        [HttpPost]
        public async Task<ActionResult<Email>> PostEmail([FromBody] EmailDTO dto)
        {
            var person = await _personRepository.GetByIdAsync(dto.PersonId);
            if (person is null)
            {
                return NotFound("Person not found");
            }
            var emailToCreate = new Email()
            {
                EmailAddress = dto.EmailAddress,
                Person = person,
            };
            await _emailRepository.AddAsync(emailToCreate);
            return Ok(emailToCreate);
        }

        [HttpGet]
        public async Task<ActionResult<List<Email>>> GetAllEmail()
        {
            return Ok(await _emailRepository.GetAllAync());
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult> PutEmail([FromRoute] long id, [FromBody] EmailDTO dto)
        {
            var person = await _personRepository.GetByIdAsync(dto.PersonId);
            if (person is null)
            {
                return NotFound("Person not found");
            }
            var emailToUpdate = await _emailRepository.GetByIdAsync(id);
            if (emailToUpdate is null)
            {
                return NotFound("Email not found");
            }
            emailToUpdate.EmailAddress = dto.EmailAddress;
            emailToUpdate.Person = person;
            emailToUpdate.LastModifiedDate = DateTime.Now;
            await _emailRepository.UpdateAsync(emailToUpdate);
            return Ok(emailToUpdate);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeleteEmail([FromRoute] long id)
        {
            var emailToDelete = await _emailRepository.GetByIdAsync(id);
            if (emailToDelete is null)
            {
                return NotFound("Email not found");
            }
            await _emailRepository.DeleteAsync(emailToDelete);
            return Ok(emailToDelete);
        }
    }
}
