using NUnit.Framework;
using System;
using GuDash.Competences.API.Domain.Competence;
using GuDash.Competences.API.Domain.Learner;



namespace Tests
{
    public class Tests
    {
        private string name = "Mêstwo";
        private Guid id = Guid.NewGuid();
        private LearnerId learner = new LearnerId(Guid.NewGuid());
        private string description = "Zwykle";
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var competence = new Competence(
                id,
                name,
                learner,
                description
                );

            Assert.NotNull(competence);
            Assert.AreEqual(1, competence.Version);
            Assert.AreEqual(new CompetenceId(id), competence.CompetenceId);
            Assert.AreEqual(learner, competence.LearnerId);
            Assert.AreEqual(name, competence.Name);
            Assert.AreEqual(description, competence.Description);
            Assert.False(competence.IsActive);
            Assert.False(competence.IsRequired);
        }
    }
}