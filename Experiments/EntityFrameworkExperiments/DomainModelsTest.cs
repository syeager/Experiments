using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using People.Core;

namespace EntityFrameworkExperiments;

public sealed class DomainModelsTest
{
    [Test]
    public void UpdateEntity()
    {
        var mapper = new Mapper(new MapperConfiguration(configure =>
        {
            configure.CreateMap<Person, PersonDao>();
            configure.CreateMap<PersonDao, Person>();
        }));

        // Get DBContext.
        using var context = new PersonContext();
        // Create new domain model.
        var model = new Person(Guid.NewGuid(), "Steve", 33);
        // Map domain model to entity (DAO).
        var entity = mapper.Map<PersonDao>(model);
        // Add entity to DBContext.
        context.Add(entity);
        // Save DBContext.
        context.SaveChanges();
        // Update domain model property.
        model.Age++;
        // Update DAO with new domain state.
        mapper.Map(model, entity);
        // Save DBContext.
        context.SaveChanges();
        // Clean up
        context.Remove(entity);
        context.SaveChanges();
    }
}

public class PersonContext : DbContext
{
    public DbSet<PersonDao> Persons { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableSensitiveDataLogging()
            .UseSqlite(@"Data Source=C:\code\Experiments\Experiments\EntityFrameworkExperiments\persons.sqlite");
    }
}