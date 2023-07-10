using AddressBook.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace AddressBook.Domain
{
    public class Phone: BaseDomainModel
    {
        [Phone]
        public string PhoneNumber { get; set; } = string.Empty;
        public long? PersonId { get; set; }
        public Person? Person { get; set; }
    }
}
