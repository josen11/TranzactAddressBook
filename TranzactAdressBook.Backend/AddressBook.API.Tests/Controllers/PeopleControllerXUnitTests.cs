using AddressBook.API.Controllers;
using AddressBook.API.DTO;
using AddressBook.Domain;
using AddressBook.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace AddressBook.API.Tests.Controllers
{
    public class PeopleControllerXUnitTests
    {
        [Fact]
        public async Task PersonRepository_GetByIdAsync_InvalidDataComparison_Fails()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AddressBookDbContext>()
                .UseInMemoryDatabase(databaseName: $"AddressBookTestDB-{Guid.NewGuid()}")
                .Options;
            using (var context = new AddressBookDbContext(options))
            {
                context.People.Add(new Person
                {
                    Id = 1,
                    FirstName = "Jose",
                    LastName = "Aguirre",
                    Phones = new List<Phone>(),
                    Addresses = new List<Address>(),
                    Emails = new List<Email>(),
                });

                context.People.Add(new Person
                {
                    Id = 2,
                    FirstName = "Nuria",
                    LastName = "Araoz",
                    Phones = new List<Phone>(),
                    Addresses = new List<Address>(),
                    Emails = new List<Email>(),
                });
                context.SaveChanges();
            }
            using (var context = new AddressBookDbContext(options))
            {
                // Act
                PersonRepository repo = new PersonRepository(context);
                var person = repo.GetByIdAsync(1);
                // Assert
                Assert.Equal(15, person.Id);
            }

        }
        [Fact]
        public async Task PeopleController_GetPerson_ValidResponseAndData_Passed()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AddressBookDbContext>()
                .UseInMemoryDatabase(databaseName: $"AddressBookTestDB-{Guid.NewGuid()}")
                .Options;
            using (var context = new AddressBookDbContext(options))
            {
                context.People.Add(new Person
                {
                    Id = 1,
                    FirstName = "Jose",
                    LastName = "Aguirre",
                    Phones = new List<Phone>(),
                    Addresses = new List<Address>(),
                    Emails = new List<Email>(),
                });
                context.People.Add(new Person
                {
                    Id = 2,
                    FirstName = "Nuria",
                    LastName = "Araoz",
                    Phones = new List<Phone>(),
                    Addresses = new List<Address>(),
                    Emails = new List<Email>(),
                });
                context.SaveChanges();
            }
            using (var context = new AddressBookDbContext(options))
            {
                // Act
                PersonRepository repo = new PersonRepository(context);
                PeopleController service = new PeopleController(repo);
                var result = await service.GetPerson(1) as ObjectResult;
                var actualresult = result.Value;

                // Assert
                Assert.IsType<OkObjectResult>(result); // Ensure 200 Response
                Assert.Equal(HttpStatusCode.OK, (HttpStatusCode)result.StatusCode); // Ensure by HttpStatusCode
                Assert.Equal("Josen", ((Person)actualresult).FirstName); // Ensure data integrity
            }
        }
        [Fact]
        public async Task PeopleController_GetPerson_NotFoundId_Passed()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AddressBookDbContext>()
                .UseInMemoryDatabase(databaseName: $"AddressBookTestDB-{Guid.NewGuid()}")
                .Options;
            using (var context = new AddressBookDbContext(options))
            {
                context.People.Add(new Person
                {
                    Id = 1,
                    FirstName = "Jose",
                    LastName = "Aguirre",
                    Phones = new List<Phone>(),
                    Addresses = new List<Address>(),
                    Emails = new List<Email>(),
                });
                context.People.Add(new Person
                {
                    Id = 2,
                    FirstName = "Nuria",
                    LastName = "Araoz",
                    Phones = new List<Phone>(),
                    Addresses = new List<Address>(),
                    Emails = new List<Email>(),
                });
                context.SaveChanges();
            }
            using (var context = new AddressBookDbContext(options))
            {
                // Act
                PersonRepository repo = new PersonRepository(context);
                PeopleController service = new PeopleController(repo);
                var result = await service.GetPerson(15) as ObjectResult;
                var actualresult = result.Value;

                // Assert
                Assert.IsType<NotFoundResult>(result); // Ensure 404 Not found
            }
        }
              

        [Fact]
        public async Task PeopleController_GetAllPeople_ValidRecordsCount_Passed()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AddressBookDbContext>()
                .UseInMemoryDatabase(databaseName: $"AddressBookTestDB-{Guid.NewGuid()}")
                .Options;
            using (var context = new AddressBookDbContext(options))
            {
                context.People.Add(new Person
                {
                    Id = 1,
                    FirstName = "Jose",
                    LastName = "Aguirre",
                    Phones = new List<Phone>(),
                    Addresses = new List<Address>(),
                    Emails = new List<Email>(),
                });

                context.People.Add(new Person
                {
                    Id = 2,
                    FirstName = "Nuria",
                    LastName = "Araoz",
                    Phones = new List<Phone>(),
                    Addresses = new List<Address>(),
                    Emails = new List<Email>(),
                });
                context.SaveChanges();
            }
            using (var context = new AddressBookDbContext(options))
            {
                // Act
                PersonRepository repo = new PersonRepository(context);
                PeopleController service = new PeopleController(repo);
                var result = await service.GetAllPeople() as ObjectResult;
                var actualresult = result.Value;
                
                // Assert
                Assert.IsType<OkObjectResult>(result); // Ensure 200 Response
                Assert.IsType<NotFoundResult>(result); // Ensure 404 Not found
                Assert.Equal(HttpStatusCode.OK, (HttpStatusCode)result.StatusCode); // Ensure by HttpStatusCode
                Assert.Equal(context.People.ToList().Count, ((List<Person>)actualresult).Count); // Ensure data integrity
            }
        }

        [Fact]
        public async Task PeopleController_PostPeople_ValidResponseAndRecordsCount_Pass()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AddressBookDbContext>()
                .UseInMemoryDatabase(databaseName: $"AddressBookTestDB-{Guid.NewGuid()}")
                .Options;
            using (var context = new AddressBookDbContext(options))
            {
                context.People.Add(new Person
                {
                    Id = 1,
                    FirstName = "Jose",
                    LastName = "Aguirre",
                    Phones = new List<Phone>(),
                    Addresses = new List<Address>(),
                    Emails = new List<Email>(),
                });

                context.People.Add(new Person
                {
                    Id = 2,
                    FirstName = "Nuria",
                    LastName = "Araoz",
                    Phones = new List<Phone>(),
                    Addresses = new List<Address>(),
                    Emails = new List<Email>(),
                });
                context.SaveChanges();
            }
            using (var context = new AddressBookDbContext(options))
            {
                // Act
                var personToCreate = new PersonDTO()
                {
                    FirstName = "New user",
                    LastName = "Last name new user",
                };
                PersonRepository repo = new PersonRepository(context);
                PeopleController service = new PeopleController(repo);
                var resultPost = await service.PostPeople(personToCreate) as ObjectResult;
                var actualPostresult = resultPost.Value;
                var result = await service.GetAllPeople() as ObjectResult;
                var actualresult = result.Value;

                Assert.IsType<OkObjectResult>(resultPost); // Ensure 200 Response
                Assert.Equal(HttpStatusCode.OK, (HttpStatusCode)result.StatusCode); // Ensure by HttpStatusCode
                Assert.Equal(3, ((List<Person>)actualresult).Count);
                Assert.Equal(3, ((Person)actualPostresult).Id);
            }
        }
    }
}
