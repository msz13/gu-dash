using AutoFixture;
using AutoFixture.AutoMoq;
using GuDash.Common.Domain.Model;
using GuDash.CompetencesService.Domain.Competences;
using GuDash.CompetencesService.Domain.Competences.Events;
using GuDash.CompetencesService.Infrastructure.Persistance;
using Marten;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using FluentAssertions;

namespace CompetencesUnitTests.application
{

    
    public class GenericRepositoryTest
    {


        [Fact]
        public void ShouldSaveEvents()
        {
            //Given
            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            var session = fixture.Freeze<Mock<DocumentSession>>();
            session.Setup(s => s.Events.StartStream(It.IsAny<string>(), It.IsAny<Array>()));

            var competencesRepository = fixture.Create<CompetenceRepository>();
                                   
            var competence = fixture.Create<Competence>();
            
            //When
            competencesRepository.Add(competence);
            
            //Than
            session.Verify(s => s.Events.StartStream(It.IsAny<string>(), It.IsAny<Array>()));

        }
    }
}
