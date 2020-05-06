using Grpc.Core;
using Grpc.Core.Interceptors;
using System.Threading.Tasks;

namespace CompetencesService.Infrastructure.Auth
{
    public class AuthenticationInterceptor : Interceptor
    {
        public override Task<TResponse> UnaryServerHandler<TRequest, TResponse>(

            TRequest request,

            ServerCallContext context,

            UnaryServerMethod<TRequest, TResponse> continuation)

        {



            return continuation(request, context);

        }
    }
}
