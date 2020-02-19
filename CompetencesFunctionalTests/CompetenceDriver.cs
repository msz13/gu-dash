using Grpc.Net.Client;


using System;
using System.Threading.Tasks;

namespace CompetencesFunctionalTests
{

    using Competences;
    using Grpc.Core;

    public class CompetenceDriver
    {

        string baseAdress = "https://localhost:5001";

        Competences.CompetencesClient client;

        CompetenceDriver()
        {
            StartClient();
        }

        private void StartClient()
        {
            var channel = GrpcChannel.ForAddress(baseAdress);
                        
            this.client = new Competences.CompetencesClient(channel);

        }

       public async Task<DefineCompetenceResponse> DefineCompetence(DefineCompetenceRequest request, string userId)
        {
            var headers = AddUserIdHeader(userId);
            return  await this.client.DefineCompetenceAsync(request, headers);
        }

        public async Task<GetCompetenceResponse> GetCompetence(GetCompetenceRequest request, string userId)
        {
            var headers = AddUserIdHeader(userId);
            return await this.client.GetCompetenceAsync(request, headers);
        }

        private Metadata AddUserIdHeader(string userId)
        {
            var headers = new Metadata();
            headers.Add("X-Authenticated-UserId", userId);

            return headers;
        }
    }
}