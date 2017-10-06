using Microsoft.VisualStudio.TestTools.UnitTesting;
using Infrastructure.TorreHanoi.ServiceAgent;

namespace Tests.TorreHanoi.Infrastructure
{
    [TestClass]
    public class SlackServiceAgentUnit
    {
        private const string CategoriaTeste = "Infrastructure/ServiceAgent/Slack";

        private ISlackServiceAgent _serviceAgent;

        [TestInitialize]
        public void SetUp()
        {
            _serviceAgent = new SlackServiceAgent(@"https://hooks.slack.com", "services/T4GF7RND6/B4GF9AFB2/7D24ynmceh2lDUvZUnSYq6Hq");
        }

        [TestMethod]
        [TestCategory(CategoriaTeste)]
        public void Post_Deve_Retornar_Sucesso()
        {
            var result = _serviceAgent.Post("Teste unitario com sucesso").Result;

            Assert.IsTrue(result);
        }
    }
}
