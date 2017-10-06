using Microsoft.VisualStudio.TestTools.UnitTesting;
using Infrastructure.TorreHanoi.Log;
using Moq;
using Infrastructure.TorreHanoi.ServiceAgent;
using System.Threading.Tasks;

namespace Tests.TorreHanoi.Infrastructure
{
    [TestClass]
    public class LoggerUnit
    {
        private const string CategoriaTeste = "Infrastructure/Logger";

        private ILogger _logger;

        [TestInitialize]
        public void SetUp()
        {
            var mockSlackServiceAgent = new Mock<ISlackServiceAgent>();
            mockSlackServiceAgent.Setup(s => s.Post(It.IsAny<string>())).Returns(() => Task.FromResult(true));

            _logger = new Logger(mockSlackServiceAgent.Object, "Erro, Fluxo".Split(','));
        }

        [TestMethod]
        [TestCategory(CategoriaTeste)]
        public void Logar_Deve_Retornar_Sucesso()
        {
            var result = _logger.Logar("teste de erro", TipoLog.Erro);

            Assert.IsTrue(result);
        }

        [TestMethod]
        [TestCategory(CategoriaTeste)]
        public void Logar_Deve_Retornar_Erro()
        {
            var result = _logger.Logar("teste de erro", TipoLog.Info);

            Assert.IsFalse(result);
        }
    }
}
