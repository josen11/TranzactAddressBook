namespace AddressBook.Domain.Common
{
    public abstract class BaseDomainModel
    {
        public long Id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? LastModifiedDate { get; set; }
    }
}
