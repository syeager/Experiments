using People.Core;

namespace Results;

public class Tests
{

    [Test]
    public void ExtractPerson()
    {
        var person = DoWork(true)
            .Extract();
        
        Assert.IsNotNull(person);
    }

    [Test]
    public void FailToExtract()
    {
        var result = DoWork(false);

        Assert.Throws<AccessingErrorResultValueException>(() => result.Extract());
    }

    [Test]
    public void OnOk()
    {
        DoWork(true)
            .OnError(Assert.Fail)
            .OnOk(Assert.NotNull);
    }
    
    [Test]
    public void OnError()
    {
        DoWork(false)
            .OnError(Assert.Pass)
            .OnOk(_ => Assert.Fail());
    }

    private static Result<Person> DoWork(bool pass)
    {
        return pass
            ? Result<Person>.Ok(new(Guid.NewGuid(), "Steve", 30))
            : Result<Person>.Error("Oops");
    }
}

