using AddressBook.Domain;

namespace AddressBook.Application.Contracts.Persistence
{
    public interface IPersonRepository : IGenericRepository<Person>
    {
        Task<IEnumerable<Person>> GetAllOrderedIncludedByDateAsync();
        Task<Person> GetIncludedByIdAsync(long id);
    }
}
