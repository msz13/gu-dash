using CompetencesService.Application.CommandHandlers;
using System;

namespace CompetencesService.Services
{
    public interface IGrpcCommandResponse
    {
        bool Succes { get; set; }
    }
    public static class GrpcResponseBulder
    {
        public static TResponse ToGrpcResponse<TResponse>(this CommandResult commandResult)
            where TResponse : class, new()
        {
            //var response = new TResponse() as IGrpcCommandResponse;
            var response = (IGrpcCommandResponse)Activator.CreateInstance(typeof(TResponse));


            if (commandResult.IsSucces == true)
            {
                response.Succes = true;

            }

            return response as TResponse;
        }
    }


}
