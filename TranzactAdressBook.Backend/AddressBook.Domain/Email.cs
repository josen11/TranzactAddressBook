using AddressBook.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace AddressBook.Domain
{
    public class Email: BaseDomainModel
    {
        [EmailAddress]
        public string EmailAddress { get; set; } = string.Empty;
        public long? PersonId { get; set; }
        public Person? Person { get; set; }
    }
}
