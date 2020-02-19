using System;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;


namespace CompetencesFunctionalTests
{
    using Competences;
    using System.Threading.Tasks;
    using FluentAssertions;

    public class RequestRespondeData {

        public RequestRespondeData() { }

        public DefineCompetenceRequest Request { get; set; }

        public string CompetenceName { get; set; }

        public DefineCompetenceResponse Response { get; set; } = new DefineCompetenceResponse();
        


    }

    [Binding]
    public class DefineAndUpdateCompetencesSteps
    {
        CompetenceDriver driver;

        string userId;

        RequestRespondeData requestRespondeData;
                
        public DefineAndUpdateCompetencesSteps(CompetenceDriver driver, RequestRespondeData requestRespondeData)
        {
            this.driver = driver;
            this.requestRespondeData = requestRespondeData;
        }

        [Given(@"logged user Mat Szczeciński of ID ""(.*)""")]
        public void GivenLoggedUserMatSzczecinskiOfID(string p0)
        {
            this.userId = p0.ToString();
        }
        
        [Given(@"Defined competence with values")]
        public async void GivenDefinedCompetenceWithValues(Table table)
        {
            var request = table.CreateInstance<DefineCompetenceRequest>();

            //this.requestRespondeData.Request.Name = request.Name;
            // this.requestRespondeData.Request.Descripion = request.Descripion;

            this.requestRespondeData.CompetenceName = request.Name;

            var response = await driver.DefineCompetence(request, this.userId);

            response.Succes.Should().BeTrue();

            
            
            
        }
        
        [When(@"Mat defines competence with values")]
        public async Task WhenMatDefinesCompetenceWithValues(Table table)
        {
            var request = table.CreateInstance<DefineCompetenceRequest>();

            this.requestRespondeData.Response = await driver.DefineCompetence(request, this.userId);
                      
        }
        
        [When(@"Mat defines competence with the same name")]
        public async void WhenMatDefinesCompetenceWithTheSameName()
        {
            var request = new DefineCompetenceRequest
            {
                Name = this.requestRespondeData.CompetenceName,
                Descripion = "1"
            };
            var response = await driver.DefineCompetence(request, this.userId);
            this.requestRespondeData.Response.Succes = response.Succes;
            this.requestRespondeData.Response.Error = response.Error;
            this.requestRespondeData.Response.CompetenceId = response.CompetenceId;

           
        }
        
        [Then(@"competence is created")]
        public void ThenCompetenceIsCreated()
        {
            this.requestRespondeData.Response.Succes.Should().BeTrue();
            this.requestRespondeData.Response.CompetenceId.Should().NotBeNullOrEmpty();
            this.requestRespondeData.Response.Error.Should().BeNull();
        }
        
        [Then(@"He can see competence with values")]
        public async Task ThenHeCanSeeCompetenceWithValues(Table table)
        {
          
            var request = new GetCompetenceRequest()
            {
                CompetenceId = this.requestRespondeData.Response.CompetenceId
           };

            var response = await this.driver.GetCompetence(request, userId);
            

            table.CompareToInstance<Competence>(response.Competence);
            
        }

        [Then(@"competence is not created and he can see error message with competence name ""(.*)""")]
        public void ThenCompetenceIsNotCreatedAndHeCanSeeErrorMessageWithCompetenceName(string p0)
        {
            this.requestRespondeData.Response.Succes.Should().BeFalse();
         //   this.requestRespondeData.Response.Error.Should().NotBeNull();
           // this.requestRespondeData.Response.Error.Message.Should().Contain(p0);
        }
    }
}
