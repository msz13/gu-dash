using CompetencesService.Application.CommandHandlers;
using CompetencesService.Services;
using GuDash.CompetencesService.Proto;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using FluentAssertions;


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
