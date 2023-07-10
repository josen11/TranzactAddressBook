using AddressBook.Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.Application.Contracts.Persistence
{
    public interface IPersonRepository : IGenericRepository<Person>
    {
        Task<IEnumerable<Person>> GetAllOrderedIncludedByDateAsync();
        Task<Person> GetIncludedByIdAsync(long id);
    }
}
