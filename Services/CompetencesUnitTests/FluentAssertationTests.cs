using FluentAssertions;
using Xunit;

namespace CompetencesUnitTests
{
    public class Person
    {
        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }

        public string Name { get; private set; }
        public int Age { get; private set; }
    }

    public class PersonWithLastName : Person
    {
        public PersonWithLastName(string name, int age, string lastName)
            : base(name, age)
        {
            LastName = lastName;
        }

        public string LastName { get; private set; }
    }
    public class FluentAssertationTests
    {
        [Fact]
        public void Be_SameInstances_Pass()
        {
            var sut = new Person("Mat", 13);
            var expected = sut;

            sut.Should().Be(expected);
        }

        [Fact]
        public void Be_DifferentInstance_NotPass()
        {
            var sut = new Person("Mat", 13);
            var expected = new Person("Mat", 13);

            sut.Should().NotBe(expected);

        }

        [Fact]
        public void Equivalent_SameObjects_Pass()
        {
            var sut = new Person("Mat", 13);
            var expected = new Person("Mat", 13);

            sut.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void Equivalent_ObjectsWithDiffProperties_Fail()
        {
            var sut = new Person("Jan", 14);
            var expected = new Person("Mat", 13);

            sut.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void Equivalent_ExtendedExpected_Fail()
        {
            var sut = new Person("Mat", 13);
            var expected = new PersonWithLastName("Mat", 13, "Sz");

            sut.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void Equivalent_ExtendedSut_Pass()
        {
            var sut = new PersonWithLastName("Mat", 13, "Sz");
            var expected = new Person("Mat", 13);

            sut.Should().BeEquivalentTo(expected);
        }
    }
}
