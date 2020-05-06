using TechTalk.SpecFlow;

namespace CompetencesFunctionalTests
{
    [Binding]
    public sealed class Hooks
    {
        // For additional details on SpecFlow hooks see http://go.specflow.org/doc-hooks

        DbTestHelper dbTestHelper;

        public Hooks()
        {
            this.dbTestHelper = new DbTestHelper();
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            //TODO: implement logic that has to run before executing each scenario
        }

        [AfterScenario]
        public async void AfterScenario()
        {
            dbTestHelper.ClearTables();
        }
    }
}
