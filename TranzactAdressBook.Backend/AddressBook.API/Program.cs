using AddressBook.Application.Contracts.Persistence;
using AddressBook.Infrastructure.Persistence;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Config DBContext in memory
builder.Services.AddDbContext<AddressBookDbContext>();
// Config DBContext with SQL Server
/*
builder.Services.AddDbContext<AddressBookDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("local");
    // options.UseSqlServer(connectionString);
});
*/

// Add services to the Dependency Container.
// builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddTransient<IPersonRepository, PersonRepository>();
builder.Services.AddTransient<IEmailRepository, EmailRepository>();
builder.Services.AddTransient<IPhoneRepository, PhoneRepository>();
builder.Services.AddTransient<IAddressRepository, AddressRepository>();

// Fix error:  A possible object cycle was detected. This can either be due to a cycle or if the object depth is larger than the maximum allowed depth of 32.
builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Cors
app.UseCors(options =>
{
    options
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
});

app.UseAuthorization();

app.MapControllers();

app.Run();
