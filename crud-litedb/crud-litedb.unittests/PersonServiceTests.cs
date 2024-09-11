using crud_litedb.Models;
using crud_litedb.Repositories;
using crud_litedb.Services;
using FluentAssertions;
using Moq;
using Xunit;

namespace crud_litedb.unittests;

public class PersonServiceTests
{
    [Fact(DisplayName = "Try to add a valid person")]
    public void AddPerson_ValidPerson()
    {
        // Arrange
        var personRepositoryMock = new Mock<IPersonRepository>();
        var userInputServiceMock = new Mock<IUserInputService>();
        userInputServiceMock.Setup(input => input.FillPerson(It.IsAny<int>())).Returns(new Person
        {
            Id = 1,
            Name = "Super Person",
            Age = 23
        });
        
        var personService = new PersonService(personRepositoryMock.Object, userInputServiceMock.Object);
        
        // Act
        personService.AddPerson();

        // Assert
        personRepositoryMock.Verify(repo => repo.AddPerson(It.IsAny<Person>()), Times.Once);
        userInputServiceMock.Verify(input => input.FillPerson(It.IsAny<int>()), Times.Once);
        personRepositoryMock.VerifyNoOtherCalls();
    }
    
    [Fact(DisplayName = "Try to add a null person")]
    public void AddPerson_NullPerson()
    {
        // Arrange
        var personRepositoryMock = new Mock<IPersonRepository>();
        var userInputServiceMock = new Mock<IUserInputService>();
        var personService = new PersonService(personRepositoryMock.Object, userInputServiceMock.Object);
        
        // Act
        personService.AddPerson();

        // Assert
        personRepositoryMock.Verify(repo => repo.AddPerson(It.IsAny<Person>()), Times.Never);
        userInputServiceMock.Verify(input => input.FillPerson(It.IsAny<int>()), Times.Once);
        personRepositoryMock.VerifyNoOtherCalls();
    }

    [Fact(DisplayName = "Try to get person with a valid ID")]
    public void GetPersonById_With_Valid_Id()
    {
        // Arrange
        var id = 1;
        var personRepositoryMock = new Mock<IPersonRepository>();
        personRepositoryMock.Setup(repo => repo.GetPersonById(id)).Returns(new Person { Id = id });
        var userInputServiceMock = new Mock<IUserInputService>();
        userInputServiceMock.Setup(input => input.GetId()).Returns(1);
        userInputServiceMock.Setup(input => input.FlowPause());
        var personService = new PersonService(personRepositoryMock.Object, userInputServiceMock.Object);
        
        // Act
        personService.GetPersonById();

        // Assert
        personRepositoryMock.Verify(repo => repo.GetPersonById(id), Times.Once);
        userInputServiceMock.Verify(input => input.GetId(), Times.Once);
        userInputServiceMock.Verify(input => input.FlowPause(), Times.Once);
        personRepositoryMock.VerifyNoOtherCalls();
        userInputServiceMock.VerifyNoOtherCalls();
    }
    
    [Fact(DisplayName = "Try to get person with a invalid ID")]
    public void GetPersonById_With_Invalid_Id()
    {
        // Arrange
        var id = 1;
        var personRepositoryMock = new Mock<IPersonRepository>();
        personRepositoryMock.Setup(repo => repo.GetPersonById(id)).Returns(new Person { Id = id });
        var userInputServiceMock = new Mock<IUserInputService>();
        var personService = new PersonService(personRepositoryMock.Object, userInputServiceMock.Object);
        
        // Act
        personService.GetPersonById();

        // Assert
        personRepositoryMock.Verify(repo => repo.GetPersonById(id), Times.Never);
        userInputServiceMock.Verify(input => input.GetId(), Times.Once);
        personRepositoryMock.VerifyNoOtherCalls();
        userInputServiceMock.VerifyNoOtherCalls();
    }

    [Fact(DisplayName = "Try to get all person with success")]
    public void GetAllPerson_Should_Write_Persons_To_Console()
    {
        // Arrange
        var persons = new List<Person>
        {
            new() { Id = 1, Name = "John", Age = 30 },
            new() { Id = 2, Name = "Paul", Age = 25 }
        };
        var stringOutput = new StringWriter();
        Console.SetOut(stringOutput);
        var stringOutputExpected = string.Empty;
        persons.ForEach(p => stringOutputExpected += $"{p}\n\n");
        
        var personRepositoryMock = new Mock<IPersonRepository>();
        personRepositoryMock.Setup(repo => repo.GetAllPerson()).Returns(persons);
        var userInputServiceMock = new Mock<IUserInputService>();
        var personService = new PersonService(personRepositoryMock.Object, userInputServiceMock.Object);
        
        // Act
        personService.GetAllPerson();

        // Asser
        stringOutputExpected.Should().Be(stringOutput.ToString());
        personRepositoryMock.Verify(repo => repo.GetAllPerson(), Times.Once);
        personRepositoryMock.VerifyNoOtherCalls();
        userInputServiceMock.Verify(u => u.FlowPause(), Times.Once);
        userInputServiceMock.VerifyNoOtherCalls();
    }
    
    [Fact(DisplayName = "Try to get all person with 0 person")]
    public void GetAllPerson_When_No_Person_Found()
    {
        // Arrange
        var stringOutput = new StringWriter();
        Console.SetOut(stringOutput);
        const string stringOutputExpected = "No person found\n";
        
        var personRepositoryMock = new Mock<IPersonRepository>();
        personRepositoryMock.Setup(repo => repo.GetAllPerson()).Returns(Array.Empty<Person>());
        var userInputServiceMock = new Mock<IUserInputService>();
        var personService = new PersonService(personRepositoryMock.Object, userInputServiceMock.Object);
        
        // Act
        personService.GetAllPerson();

        // Asser
        stringOutputExpected.Should().Be(stringOutput.ToString());
        personRepositoryMock.Verify(m => m.GetAllPerson(), Times.Once);
        personRepositoryMock.VerifyNoOtherCalls();
        userInputServiceMock.Verify(u => u.FlowPause(), Times.Once);
        userInputServiceMock.VerifyNoOtherCalls();
    }

    [Fact(DisplayName = "Try to update a valid person")]
    public void UpdatePerson_WithValidId_ShouldUpdatePerson()
    {
        // Arrange
        var userInputServiceMock = new Mock<IUserInputService>();
        var personRepositoryMock = new Mock<IPersonRepository>();

        var personService = new PersonService(personRepositoryMock.Object, userInputServiceMock.Object);

        var id = 1;
        var person = new Person { Id = id, Name = "A Person"};
        var personUpdated = new Person { Id = id, Name = "The Person"};
        userInputServiceMock.Setup(s => s.GetId()).Returns(id);
        personRepositoryMock.Setup(repo => repo.GetPersonById(id)).Returns(person);
        userInputServiceMock.Setup(s => s.FillPerson(id)).Returns(personUpdated);
        personRepositoryMock.Setup(repo => repo.UpdatePerson(person)).Returns(true);

        // Act
        personService.UpdatePerson();

        // Assert
        personRepositoryMock.Verify(p => p.GetPersonById(It.IsAny<int>()), Times.Once);
        personRepositoryMock.Verify(p => p.UpdatePerson(personUpdated), Times.Once);
        personRepositoryMock.VerifyNoOtherCalls();
        userInputServiceMock.Verify(m => m.GetId(), Times.Once);
        userInputServiceMock.Verify(m => m.FillPerson(id), Times.Once);
        userInputServiceMock.Verify(m => m.FlowPause(), Times.Once);
        userInputServiceMock.VerifyNoOtherCalls();
    }

    [Fact(DisplayName = "Try to update a invalid person")]
    public void UpdatePerson_WithInvalidId_ShouldNotUpdatePerson()
    {
        // Arrange
        var stringOutput = new StringWriter();
        Console.SetOut(stringOutput);
        
        var userInputServiceMock = new Mock<IUserInputService>();
        var personRepositoryMock = new Mock<IPersonRepository>();
        
        var personService = new PersonService(personRepositoryMock.Object, userInputServiceMock.Object);

        var id = 1;
        userInputServiceMock.Setup(service => service.GetId()).Returns(id);
        personRepositoryMock.Setup(repo => repo.GetPersonById(id)).Returns((Person)null!);

        // Act
        personService.UpdatePerson();

        // Assert
        stringOutput.ToString().Should().NotBeNull();
        personRepositoryMock.Verify(p => p.GetPersonById(It.IsAny<int>()), Times.Once);
        userInputServiceMock.Verify(m => m.GetId(), Times.Once);
        userInputServiceMock.Verify(m => m.FlowPause(), Times.Once);
        personRepositoryMock.VerifyNoOtherCalls();
        userInputServiceMock.VerifyNoOtherCalls();
    }

    [Fact(DisplayName = "Try to delete a valid person")]
    public void Delete_Valid_Person()
    {
        // Arrange
        var id = 1;
        var personRepositoryMock = new Mock<IPersonRepository>();
        personRepositoryMock.Setup(p => p.DeletePerson(id)).Returns(true);
        var userInputServiceMock = new Mock<IUserInputService>();
        userInputServiceMock.Setup(u => u.GetId()).Returns(id);
        var personService = new PersonService(personRepositoryMock.Object, userInputServiceMock.Object);
        
        // Act
        personService.DeletePerson();

        // Assert
        personRepositoryMock.Verify(repo =>
            repo.DeletePerson(id), Times.Once);
        userInputServiceMock.Verify(u => u.GetId(), Times.Once);
        userInputServiceMock.VerifyNoOtherCalls();
        personRepositoryMock.VerifyNoOtherCalls();
    }
    
    [Fact(DisplayName = "Try to delete a invalid person")]
    public void Delete_invalid_Person()
    {
        // Arrange
        var stringOutput = new StringWriter();
        Console.SetOut(stringOutput);
        var id = 1;
        var personRepositoryMock = new Mock<IPersonRepository>();
        personRepositoryMock.Setup(p => p.DeletePerson(id)).Returns(false);
        var userInputServiceMock = new Mock<IUserInputService>();
        userInputServiceMock.Setup(u => u.GetId()).Returns(id);
        var personService = new PersonService(personRepositoryMock.Object, userInputServiceMock.Object);
        
        // Act
        personService.DeletePerson();

        // Assert
        stringOutput.ToString().Should().NotBeNull();
        personRepositoryMock.Verify(repo =>
            repo.DeletePerson(id), Times.Once);
        userInputServiceMock.Verify(u => u.GetId(), Times.Once);
        userInputServiceMock.Verify(u => u.FlowPause(), Times.Once);
        userInputServiceMock.VerifyNoOtherCalls();
        personRepositoryMock.VerifyNoOtherCalls();
    }
}
