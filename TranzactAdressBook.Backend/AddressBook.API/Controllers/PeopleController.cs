using AddressBook.API.DTO;
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
        private readonly AddressBookDbContext _addressBookDbContext;

        public PeopleController(AddressBookDbContext addressBookDbContext)
        {
            _addressBookDbContext = addressBookDbContext;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Person>> GetPerson([FromRoute] long id)
        {
            var person = await _addressBookDbContext.People.FirstOrDefaultAsync(q => q.Id == id);
            if (person is null)
            {
                return NotFound("Person not found");
            }
            return Ok(person);
        }

        [HttpPost]
        public async Task<ActionResult<Person>> PostPeople([FromBody] PersonDTO dto)
        {
            var newPerson = new Person()
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
            };
            _addressBookDbContext.People.Add(newPerson);
            await _addressBookDbContext.SaveChangesAsync();
            return CreatedAtAction(
              "GetPerson",
              new { id = newPerson.Id },
              newPerson);
            // return Ok("Person created");
        }

        [HttpGet]
        public async Task<ActionResult<List<Person>>> GetAllPeople()
        {
            var people = await _addressBookDbContext.People.OrderByDescending(p => p.LastModifiedDate).ThenByDescending(p => p.CreatedDate).ToListAsync();
            return Ok(people);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult> PutPeople([FromRoute] long id, [FromBody] PersonDTO dto)
        {
            var person = await _addressBookDbContext.People.FirstOrDefaultAsync(q => q.Id == id);
            if (person is null)
            {
                return NotFound("Person not found");
            }
            person.FirstName = dto.FirstName;
            person.LastName = dto.LastName;
            person.LastModifiedDate = DateTime.Now;
            await _addressBookDbContext.SaveChangesAsync();
            return Ok("Person updated");
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeletePerson([FromRoute] long id)
        {
            var personToDelete = await _addressBookDbContext.People.FindAsync(id);
            if (personToDelete is null)
            {
                return NotFound("Person not found");
            }
            _addressBookDbContext.People.Remove(personToDelete);
            await _addressBookDbContext.SaveChangesAsync();
            return Ok(personToDelete);
        }

        [HttpPost]
        [Route("delete")]
        // people/delete?ids=1&ids=3&ids=33
        public async Task<ActionResult> DeletePeople ([FromQuery] long[] ids)
        {
            var peopleToDelete = new List<Person>();
            foreach (var id in ids)
            {
                var person = await _addressBookDbContext.People.FindAsync(id);
                if (person == null)
                {
                    return NotFound("Person not found");
                }
                peopleToDelete.Add(person);
            }
            _addressBookDbContext.People.RemoveRange(peopleToDelete);
            await _addressBookDbContext.SaveChangesAsync();
            return Ok(peopleToDelete);
        }
    }
}
