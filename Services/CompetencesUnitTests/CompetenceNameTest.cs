using FluentAssertions;
using GuDash.CompetencesService.Domain.Competences;
using System;
using Xunit;

namespace CompetencesUnitTests
{
    public class CompetenceValueObjectsTests
    {
        [Fact]
        public void Competence_Name_WithEmtyName_Fails()
        {
            Action action = () => new CompetenceName("");

            action.Should().ThrowExactly<InvalidOperationException>();
        }

        [Fact]
        public void Competence_Name_WithLenghtMoreThan120_Fails()
        {
            Action action = () => new CompetenceName("LoremImpsumLoremImpsumLoremImpsumLoremImpsumLoremImpsumLoremImpsumLoremImpsumLoremImpsumLoremImpsumLoremImpsumLoremImpsum");

            action.Should().ThrowExactly<InvalidOperationException>();
        }
    }
}
