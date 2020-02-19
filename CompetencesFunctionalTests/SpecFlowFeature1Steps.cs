using System;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace CompetencesFunctionalTests
{
    [Binding]
    public class SpecFlowFeature1Steps
    {
        CompetenceDriver driver;



        [Given(@"User of ID '(.*)'")]
        public void GivenUserOfID(int p0)
        {

        }
        
        [Given(@"competence name '(.*)'")]
        public void GivenCompetenceName(string p0)
        {
           
        }
        
        [When(@"Request")]
        public void WhenRequest()
        {
            
        }

        [Then(@"can see competence with properties")]
        public void ThenCanSeeCompetenceWithProperties(Table table)
        {
           
           
        }
    }
}
