using AddressBook.Domain.Common;

namespace AddressBook.Domain
{
    public class Address: BaseDomainModel
    {
        public string HomeAddress { get; set; } = string.Empty;
        public long? PersonId { get; set; }
        public Person? Person { get; set; }
    }
}
