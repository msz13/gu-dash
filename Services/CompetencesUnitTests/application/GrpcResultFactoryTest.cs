using CompetencesService.Application.CommandHandlers;
using CompetencesService.Services;
using FluentAssertions;
using GuDash.CompetencesService.Proto;
using Xunit;


namespace CompetencesUnitTests.application
{
    public class GrpcResultFactoryTest
    {
        [Fact]
        public void Should_Create_Success_Result()
        {
            //Given
            var commandResult = new CommandResult();
            //commandResult.IsSucces = true;

            //When
            var response = commandResult.ToGrpcResponse<DefineCompetenceResponse>();

            //Then
            response.Succes.Should().BeTrue();

        }
    }
}
