
using AddressBook.Application.Contracts.Persistence;
using AddressBook.Domain;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace AddressBook.Infrastructure.Persistence
{
    public class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        public PersonRepository(AddressBookDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Person>> GetAllAyncOrderedByDate()
        {
            return await _context.People!.OrderByDescending(p => p.LastModifiedDate).ThenByDescending(p => p.CreatedDate).ToListAsync();
        }
    }
}
