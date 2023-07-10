using AddressBook.Application.Contracts.Persistence;
using AddressBook.Domain;

namespace AddressBook.Infrastructure.Persistence
{
    public class AddressRepository: GenericRepository<Address>, IAddressRepository
    {
        public AddressRepository(AddressBookDbContext context) : base(context)
        {

        }
    }
}
