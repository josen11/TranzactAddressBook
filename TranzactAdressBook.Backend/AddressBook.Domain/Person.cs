using AddressBook.Domain.Common;

namespace AddressBook.Domain
{
    public class Person: BaseDomainModel
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public virtual ICollection<Address>? Addresses { get; set; }
        public virtual ICollection<Phone>? Phones { get; set; }
        public virtual ICollection<Email>? Emails { get; set; }

    }
}