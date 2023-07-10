using AddressBook.Domain.Common;

namespace AddressBook.Domain
{
    public class Person: BaseDomainModel
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public ICollection<Address> Addresses { get; set; } = new List<Address>();
        public ICollection<Phone> Phones { get; set; } = new List<Phone>();
        public ICollection<Email> Emails { get; set; } = new List<Email>();

    }
}