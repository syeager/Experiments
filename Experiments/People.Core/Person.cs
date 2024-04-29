namespace People.Core;

public class Person(Guid id, string name, int age)
{
    public int Age { get; set; } = age;
    public Guid Id { get; } = id;
    public string Name { get; } = name;
}

public class PersonDao : Entity
{
    public int Age { get; set; }
    public string Name { get; set; }
}

public abstract partial class Entity
{
    public Guid Id { get; set; }
}

public interface IUpdateable<TFrom>
{

}