using GuDash.CompetencesService.Application.CommandHandlers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xunit;
using FluentAssertions;
using System.Threading.Tasks;
using Moq;
using GuDash.CompetencesService.Domain.Competences;
using GuDash.CompetencesService.Domain.Learner;
using GuDash.CompetencesService.Infrastructure.Persistance;
using AutoFixture;
using AutoFixture.AutoMoq;
using CSharpFunctionalExtensions;
using CompetencesService.Application.CommandHandlers;
using GuDash.Common.Domain.Model;
using GuDash.CompetencesService.Domain.Learner.Events;

namespace CompetencesUnitTests.application
{
    public class DefineCompetenceCommandHandlerTest
    {
        DefineCompetenceCommand CreateCommand(string competenceName)
        {
            return new DefineCompetenceCommand(
                 Guid.NewGuid().ToString(),
                competenceName,
                "",
                false
                );
        }
      [Fact]
      public async Task  Handles_With_Succes()
        {
            //Given

            var command = CreateCommand("Uprzejmość");

            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            var mockDocumentStore = fixture.Freeze<Mock<ICompetencesStore>>();              
                       
            mockDocumentStore.Setup(r => r.Competences.Add(It.IsAny<Competence>()));
                                    
            var handler = new DefineCompetenceCommandHandler(mockDocumentStore.Object);

            //When
            var result = await handler.Handle(command, new CancellationToken());

            //Than
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();

            
            mockDocumentStore.Verify(r => r.Competences.Add(
                It.Is<Competence>(c => c.GetSnapshot().Name==command.Name)), 
                Times.AtLeastOnce);
        }

        [Fact]
        public async void GivenNoLearner_ShouldCreateNewLearner()
        {
            //given
            var command = CreateCommand("Uprzejmość");
            var fixture = new Fixture().Customize(new AutoMoqCustomization());
                     
            
            var learnerRepo = fixture.Freeze<Mock<ILearnerRepository>>();
            learnerRepo.Setup(r => r.LearnerOfId(It.Is<LearnerId>(id => id.Id==command.LearnerId))).Returns(null as Task<Learner>);
           // store.Setup(s => s.Learner).Returns(learnerRepo.Object);

           // store.Setup(s => s.Learner.Add(It.IsAny<Learner>()));
            //var store = fixture.Freeze<Mock<ICompetencesStore>>();

            var handler = fixture.Create<DefineCompetenceCommandHandler>();

            //when 
            var result = await handler.Handle(command, new CancellationToken());

            //than
            learnerRepo.Verify(r => r.Add(It.IsAny<Learner>()));

        }

        [Fact]
        public async void GivenSameName_ReturnFailureResult()
        {
            //given
            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            var name = "Uprzejmość";
            var command = CreateCommand(name);
            

          /*  var learner = fixture.Create<Learner>();
            learner.Setup(l => l.DefineCompetence(It.IsAny<CompetenceId>(), It.Is<string>(n => n == name), It.IsAny<string>(), It.Is<bool>(r => r == command.IsRequired)))
                .Returns(Result.Failure<Competence, Error>(new Error("NON_UNIQUE_COMPETENCE_NAME", $"Competence with name {name} already exists"))); */

         var learner = new Learner();
            var learnerId = new LearnerId();
            learner.LoadEvents(
                new List<IDomainEvent> { 
                    new LearnerCreated(learnerId, "Erope/Warsaw"),
                    new LearnerCompetenceDefined(learnerId, new CompetenceId(), name, false)                
            });

            var learnerRepo = fixture.Freeze<Mock<ILearnerRepository>>();
            learnerRepo.Setup(r => r.LearnerOfId(It.IsAny<LearnerId>())).Returns(Task.FromResult(learner));
            var store = fixture.Freeze<Mock<ICompetencesStore>>();
            store.Setup(s => s.Learner).Returns(learnerRepo.Object);

            var handler = fixture.Create<DefineCompetenceCommandHandler>();

            //when
            var result = await handler.Handle(command, new CancellationToken());

            //than
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Be(new Error("NON_UNIQUE_COMPETENCE_NAME", $"Competence with name {name} already exists"));                                        

        }


    }
}
