using AddressBook.Domain;
using Microsoft.EntityFrameworkCore;

namespace AddressBook.Infrastructure.Persistence
{
    public class AddressBookDbContext : DbContext
    {
        public AddressBookDbContext(DbContextOptions<AddressBookDbContext> options) : base(options)
        {
        }

        // Configure InMemory DB
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "InMemoryAddressBook");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>()
                .HasMany(e => e.Emails)
                .WithOne(e => e.Person)
                .HasForeignKey(e => e.PersonId)
               .IsRequired(false);

            modelBuilder.Entity<Person>()
               .HasMany(e => e.Phones)
               .WithOne(e => e.Person)
               .HasForeignKey(e => e.PersonId)
              .IsRequired(false);

            modelBuilder.Entity<Person>()
               .HasMany(e => e.Addresses)
               .WithOne(e => e.Person)
               .HasForeignKey(e => e.PersonId)
              .IsRequired(false);
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Email> Emails { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Phone> Phones { get; set; }
    }
}