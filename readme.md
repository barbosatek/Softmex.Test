# AutoMock, test what matters!
This framework allows testing units in a cleaner way by removing noisy code and focus on the code that matters. It allows the developer to:
* Completely ignore dependencies not being used in the test unit, even if they're needed in the constructor.
* Ignore delcaring mocks, the framework keeps track of them via the `The<T>()` method.
* Ignore instantiating the target class, the framework provides an instantes
* Not worry about breaking tests when changing the parameter order in constructors.

The framework currently provides implementations for Moq and NSubstitute, but it is flexible enough to add any other mocking framework.

## Properties and Methods
### `TestFor<T>` (Base Class)
| Property / Method        | Description           |
| ------------- |-------------|
| `Target`      | Builds and returns an instance of the class under tests. If the instance was already built, it returns it and doesn't re-create it. |
| `T GetDependency<T>()` | Gets the dependency of `T`, if the dependency wasn't set, it returns null.|
| `void SetDependency<T>(T dependency)` | Sets the dependency of `T`, if the dependency had already been set, it overrides it.      |
| `void SetDependency(Type type, object dependency)` | Sets the dependency of provided type, if the dependency had already been set, it overrides it.      |

### `MoqTestFor<T>` (Moq implementation)
| Property / Method        | Description           |
| ------------- |-------------|
| `Mock<T> The<T>()` | For `MoqTestFor<T>`, this method creates and sets a `Mock` instance. If the instance was already created, it returns it.|
| `Mock The(Type type)` | For `MoqTestFor<T>`, this method creates and sets a `Mock` instance. If the instance was already created, it returns it.      |

### `NSubstituteTestFor<T>` (NSubstitute implementation)
| Property / Method        | Description           |
| ------------- |-------------|
| `T The<T>()` | TDB      |
| `object The(Type type)` | TDB      |

## Moq Examples
The following example shows how intantiating the mocks and the target class is not necessary.
```
  public class MoqTests : MoqTestFor<ClassUnderTest>
  {
    [Fact]
    public void UnitTestExample()
    {
      // Arrange
      The<IDependency>().Setup(x => x.GetValue()).Returns(Guid.NewGuid().ToString());

      // Act
      var isValid = Target.IsDependencyValid();

      // Assert
      isValid.Should().BeTrue();
    }
  }
```

In comparison, this would be the "traditional" way to write the unit test with Moq

```
    [Fact]
    public void UnitTestExample()
    {
      // Arrange
      var dependencyMock = new Mock<IDependency>();
      var secondDependencyMock = new Mock<ISecondDependency>();
      var thirdDependencyMock = new Mock<IThirdDependency>();
      var abstractDependencyMock = new Mock<AbstractDependency>();

      dependencyMock.Setup(x => x.GetValue()).Returns(Guid.NewGuid().ToString());
	  
      var target = new ClassUnderTest(dependencyMock.Object, secondDependencyMock.Object, thirdDependencyMock.Object, abstractDependencyMock.Object);

      // Act
      var isValid = target.IsDependencyValid();

      // Assert
      isValid.Should().BeTrue();
    }
```

Notice that AutoMock doesn't require declaring dependencies that aren't being used in the test unit.

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