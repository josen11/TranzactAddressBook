
using AddressBook.Application.Contracts.Persistence;
using AddressBook.Domain;

namespace AddressBook.Infrastructure.Persistence
{
    public class EmailRepository : GenericRepository<Email>, IEmailRepository
    {
        public EmailRepository(AddressBookDbContext context) : base(context)
        {

        }
    }
}
