using AddressBook.Domain.Common;

namespace AddressBook.Domain
{
    public class Email: BaseDomainModel
    {
        public string? EmailAddress { get; set; }
        public long PersonId { get; set; }
        public virtual Person? Person { get; set; }
    }
}
