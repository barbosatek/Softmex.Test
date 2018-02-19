# AutoMock framework
This framework allows a cleaner way to mock dependencies on top of mocking frameworks. This allows developers to focus on the code being tested rather than on the mocking setup and its maintenance. This removes code declaring mocks, and broken tests when constructor parameters change. The framework works by looking at the class under test, then it takes its constructor with the longest parameters, then if the parameter was defined, it uses it, otherwise it uses reflection to create a mock with the provided framework.

## Properties and Methods
| Property / Method        | Description           |
| ------------- |-------------|
| `Target`      | Builds and returns an instance of the class under tests. If the instance was already built, it returns it and doesn't re-create it. |
| `Mock<T2> The<T2>()` | For `MoqTestFor<T>`, this method creates and sets a `Mock` instance. If the instance was already created, it returns it.|
| `Mock The(Type type)` | For `MoqTestFor<T>`, this method creates and sets a `Mock` instance. If the instance was already created, it returns it.      |
| `T2 The<T2>()` | TDB      |
| `object The(Type type)` | TDB      |
| `T2 GetDependency<T2>()` | Gets the dependency of `T2`, if the dependency wasn't set, it returns null.|
| `void SetDependency<T2>(T2 dependency)` | Sets the dependency of `T2`, if the dependency had already been set, it overrides it.      |
| `void SetDependency(Type type, object dependency)` | Sets the dependency of provided type, if the dependency had already been set, it overrides it.      |

## Moq Examples
```
  public class MoqTests : MoqTestFor<ClassUnderTest>
  {
    [Fact]
    public void UnitTestExample()
    {
      // Arrange
      The<IDependency>().Setup(x => x.GetValue()).Returns(Guid.NewGuid().ToString());

      // Act
      var isValid = Target.AreDependenciesValid();

      // Assert
      isValid.Should().BeTrue();
    }
  }
```
## NSubstitute Examples
```
  public class NSubstituteTests : NsubstituteTestFor<ClassUnderTest>
  {
    [Fact]
    public void UnitTestExample()
    {
      // Arrange
      The<IDependency>().GetValue().Returns(Guid.NewGuid().ToString());

      // Act
      var isValid = Target.AreDependenciesValid();

      // Assert
      isValid.Should().BeTrue();
    }
  }
```