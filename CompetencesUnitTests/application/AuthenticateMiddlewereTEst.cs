using CompetencesService.Infrastructure.Auth;
using Grpc.Core;
using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Grpc.Core.Testing;
using FluentAssertions;
using System.Security.Claims;
using System.Security.Authentication;
using System.Threading.Tasks;

namespace CompetencesUnitTests.application
{
    public class AuthenticateMiddlewereTest
    {
        [Fact]
        public async void WithValidParams_ShouldAddUserObject()
        {
            var mockDelegate = new Mock<RequestDelegate>();
           
            var auth = new AuthenticationMiddlewere(mockDelegate.Object);

            var id = "12345";
            var context = new DefaultHttpContext();
            context.Request.Headers.Add("X-Authenticated-UserID", id);

            await auth.InvokeAsync(context);

            context.User.FindFirstValue(ClaimTypes.NameIdentifier).Should().Be(id);
        }

        [Fact]
        public void WithNoHeader_RaiseException()
        {
            var mockDelegate = new Mock<RequestDelegate>();

            var auth = new AuthenticationMiddlewere(mockDelegate.Object);

            var context = new DefaultHttpContext();

            
            Func<Task> action = async () => await auth.InvokeAsync(context);

            action.Should().Throw<AuthenticationException>();


        }

        [Fact]
        public void WithEmptyHeader_RaiseException()
        {
            var mockDelegate = new Mock<RequestDelegate>();

            var auth = new AuthenticationMiddlewere(mockDelegate.Object);

            var context = new DefaultHttpContext();
            context.Request.Headers.Add("X-Authenticated-UserId", "");

            Func<Task> action = async () => await auth.InvokeAsync(context);

            action.Should().Throw<AuthenticationException>();


        }

    }

   
}
