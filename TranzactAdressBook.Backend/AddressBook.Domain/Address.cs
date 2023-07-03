using AddressBook.Domain.Common;

namespace AddressBook.Domain
{
    public class Address: BaseDomainModel
    {
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? Type { get; set; }
        public long PersonId { get; set; }
        public virtual Person? Person { get; set; }
    }
}
