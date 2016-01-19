﻿using Moq;
using NUnit.Framework;
using Softmex.Test.Tests.Artifacts;

namespace Softmex.Test.Tests
{
    /// TODO: Add tests for ICollection<IDependencies>

    [TestFixture]
    public class TestForAbstractDependencyTests : TestFor<ClassWithSingleAbstractParameterConstructor>
    {
        [Test]
        public void Should_Initialize_Instance()
        {
            Assert.IsNotNull(Target);
            Assert.IsNotNull(Target.GetDependecy());
            Assert.IsTrue(Target.GetType() == typeof(ClassWithSingleAbstractParameterConstructor));
        }
    }

    [TestFixture]
    public class TestForSingleDependencyTests : TestFor<ClassWithSingleInterfaceConstructor>
    {
        [Test]
        public void Should_Initialize_Instance()
        {
            Assert.IsNotNull(Target);
            Assert.IsNotNull(Target.GetDependecy());
            Assert.IsTrue(Target.GetType() == typeof(ClassWithSingleInterfaceConstructor));
        }

        [Test]
        public void Should_Create_Instances_On_Getter()
        {
            string value = "test";
            The<IDependencyA>().Setup(x => x.Value).Returns(value);

            Assert.IsNotNull(Target);
            Assert.AreEqual(Target.DereferencedValueOnConstruction, value);
            Assert.IsNotNull(Target.GetDependecy());
            Assert.IsTrue(Target.GetType() == typeof(ClassWithSingleInterfaceConstructor));
        }
    }

    [TestFixture]
    public class TestForNoDependenciesTests : TestFor<ClassWithEmptyDefaultConstructor>
    {
        [Test]
        public void Should_Initialize_Instance()
        {
            Assert.IsNotNull(Target);
            Assert.IsTrue(Target.GetType() == typeof(ClassWithEmptyDefaultConstructor));
        }

        [Test]
        public void Should_Add_Any_Mocks()
        {
            var dependency = The<IDependencyA>();
            Assert.IsTrue(dependency.GetType() == typeof(Mock<IDependencyA>));
        }

        [Test]
        public void Should_Persist_Mocks_As_Singletons()
        {
            var expected = "A";
            var dependency = The<IDependencyA>();
            dependency.Setup(x => x.Value).Returns(expected);

            var anotherDependency = The<IDependencyA>();
            Assert.AreEqual("A", anotherDependency.Object.Value);
        }
    }
}