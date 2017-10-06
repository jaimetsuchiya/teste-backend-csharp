using System;
using System.Linq;
using Infrastructure.TorreHanoi.Log;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Tests.TorreHanoi.Domain
{
    [TestClass]
    public class PinoUnit
    {
        private const string CategoriaTeste = "Domain/Pino";

        private Mock<ILogger> _mockLogger;

        [TestInitialize]
        public void SetUp()
        {
            _mockLogger = new Mock<ILogger>();
            _mockLogger.Setup(s => s.Logar(It.IsAny<string>(), It.IsAny<TipoLog>()));
        }

        [TestMethod]
        [TestCategory(CategoriaTeste)]
        public void Construtor_Deve_Retornar_Sucesso()
        {
            var torre = new global::Domain.TorreHanoi.TorreHanoi(3, _mockLogger.Object);

            Assert.IsNotNull(torre);
            Assert.IsNotNull(torre.Destino);
            Assert.IsNotNull(torre.Origem);
            Assert.IsNotNull(torre.Intermediario);
            Assert.AreEqual(torre.Origem.Tipo, global::Domain.TorreHanoi.TipoPino.Origem);
            Assert.AreEqual(torre.Destino.Tipo, global::Domain.TorreHanoi.TipoPino.Destino);
            Assert.AreEqual(torre.Intermediario.Tipo, global::Domain.TorreHanoi.TipoPino.Intermediario);
            Assert.AreEqual(torre.Intermediario.Discos.Count, 0);
            Assert.AreEqual(torre.Destino.Discos.Count, 0);
            Assert.AreEqual(torre.Origem.Discos.Count, 3);
        }

        [TestMethod]
        [TestCategory(CategoriaTeste)]
        public void RemoverDisco_Deverar_Retornar_Sucesso()
        {
            var torre = new global::Domain.TorreHanoi.TorreHanoi(3, _mockLogger.Object);

            Assert.IsNotNull(torre);
            Assert.IsNotNull(torre.Origem);
            Assert.AreEqual(torre.Origem.Discos.Count, 3);

            torre.Origem.RemoverDisco();

            Assert.AreEqual(torre.Origem.Discos.Count, 2);
        }

        [TestMethod]
        [TestCategory(CategoriaTeste)]
        public void AdicionarDisco_Deverar_Retornar_Sucesso()
        {
            var torre = new global::Domain.TorreHanoi.TorreHanoi(3, _mockLogger.Object);

            Assert.IsNotNull(torre);
            Assert.IsNotNull(torre.Intermediario);
            Assert.AreEqual(torre.Intermediario.Discos.Count, 0);

            torre.Intermediario.AdicionarDisco(torre.Discos.First());

            Assert.AreEqual(torre.Intermediario.Discos.Count, 1);
        }

        [TestMethod]
        [TestCategory(CategoriaTeste)]
        [ExpectedException(typeof(Exception), "Não é possivel adicionar um disco maior em cima de um menor")]
        public void AdicionarDisco_Deverar_Retornar_Erro()
        {
            var torre = new global::Domain.TorreHanoi.TorreHanoi(3, _mockLogger.Object);

            Assert.IsNotNull(torre);
            Assert.IsNotNull(torre.Intermediario);
            Assert.AreEqual(torre.Intermediario.Discos.Count, 0);

            torre.Intermediario.AdicionarDisco(torre.Discos.Last());

            Assert.AreEqual(torre.Intermediario.Discos.Count, 1);

            torre.Intermediario.AdicionarDisco(torre.Discos.First());
        }
    }
}
