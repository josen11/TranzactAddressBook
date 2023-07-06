
using AddressBook.Application.Contracts.Persistence;
using AddressBook.Domain;

namespace AddressBook.Infrastructure.Persistence
{
    public class PhoneRepository: GenericRepository<Phone>,IPhoneRepository
    {
        public PhoneRepository(AddressBookDbContext context) : base(context)
        {

        }
    }
}
