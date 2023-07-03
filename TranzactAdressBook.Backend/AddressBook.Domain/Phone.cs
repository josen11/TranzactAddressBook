using AddressBook.Domain.Common;

namespace AddressBook.Domain
{
    public class Phone: BaseDomainModel
    {
        public string? PhoneNumber { get; set; }
        public long PersonId { get; set; }
        public virtual Person? Person { get; set; }
    }
}
