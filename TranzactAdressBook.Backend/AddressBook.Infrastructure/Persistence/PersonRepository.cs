using AddressBook.Application.Contracts.Persistence;
using AddressBook.Domain;
using Microsoft.EntityFrameworkCore;

namespace AddressBook.Infrastructure.Persistence
{
    public class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        public PersonRepository(AddressBookDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Person>> GetAllOrderedIncludedByDateAsync()
        {
            return await _context.People!
                .Include(c => c.Emails)
                .Include(c => c.Phones)
                .Include(c => c.Addresses)
                .OrderByDescending(p => p.LastModifiedDate)
                .ThenByDescending(p => p.CreatedDate)
                .ToListAsync();
        }
        public async Task<Person> GetIncludedByIdAsync(long id)
        {
            return await _context.People!
                .Where(p => p.Id == id)
                .Include(c => c.Phones)
                .Include(c => c.Addresses)
                .Include(c => c.Emails)
                .FirstOrDefaultAsync();
        }
    }
}
