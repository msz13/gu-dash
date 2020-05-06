using Competences;
using CSharpFunctionalExtensions;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;


namespace CompetencesFunctionalTests
{
    public class RestCompetenceDriver : ICompetenceDriver
    {
        HttpClient _httpClient = new HttpClient();
        public RestCompetenceDriver()
        {
        }

        public async Task<DefineCompetenceResponse> DefineCompetence(DefineCompetenceRequest request, string userId)
        {
            var body = JsonSerializer.Serialize(request);

            var req = new StringContent(body);
            req.Headers.ContentType = new MediaTypeHeaderValue('application/json');
            req.Headers.Add("X-Authenticated-AccauntId", userId);

            var response = await _httpClient.PostAsync("https://localhost:5001", req);



            return response.IsSuccessStatusCode ? 
                Result.Ok<Uri, Error>(response.Headers.Location) 
                : Result.Failure<Uri, Error>( JsonSerializer.Deserialize<Error>(await response.Content.ReadAsStringAsync()));
        }

        public Task<GetCompetenceResponse> GetCompetence(GetCompetenceRequest request, string userId)
        {
            throw new NotImplementedException();
        }

        public Task<RenameCompetenceResponse> RenameCompetence(RenameCompetenceRequest request, string userId)
        {
            throw new NotImplementedException();
        }
    }
}
