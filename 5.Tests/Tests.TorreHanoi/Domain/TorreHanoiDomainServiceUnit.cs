using System;
using System.Collections.Generic;
using Domain.TorreHanoi.Interface.Repository;
using Domain.TorreHanoi.Interface.Service;
using Domain.TorreHanoi.Service;
using Infrastructure.TorreHanoi.Log;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Tests.TorreHanoi.Domain
{
    [TestClass]
    public class TorreHanoiDomainServiceUnit
    {
        private const string CategoriaTeste = "Domain/Service/TorreHanoi";

        private ITorreHanoiDomainService _service;

        [TestInitialize]
        public void SetUp()
        {
            var mockLogger = new Mock<ILogger>();
            mockLogger.Setup(s => s.Logar(It.IsAny<string>(), It.IsAny<TipoLog>()));

            var mockQueueRepository = new Mock<IQueueRepository>();
            mockQueueRepository.Setup(s => s.AdicionarNaFila(It.IsAny<global::Domain.TorreHanoi.TorreHanoi>()));
            mockQueueRepository.Setup(s => s.ObterPor(It.IsAny<Guid>())).Returns(new global::Domain.TorreHanoi.TorreHanoi(3, mockLogger.Object));
            mockQueueRepository.Setup(s => s.ObterTodos()).Returns(() => new List<global::Domain.TorreHanoi.TorreHanoi> { new global::Domain.TorreHanoi.TorreHanoi(3, mockLogger.Object) });

            _service = new TorreHanoiDomainService(mockQueueRepository.Object, mockLogger.Object);
        }

        [TestMethod]
        [TestCategory(CategoriaTeste)]
        public void Criar_Deve_Retornar_Sucesso()
        {
            var idProcesso = _service.Criar(3);

            Assert.IsNotNull(idProcesso);
            Assert.AreNotEqual(idProcesso, new Guid());
        }

        [TestMethod]
        [TestCategory(CategoriaTeste)]
        public void ObterPor_Deverar_Retornar_Sucesso()
        {
            var response = _service.ObterPor(Guid.NewGuid());

            Assert.IsNotNull(response);
            Assert.AreEqual(response.Status, global::Domain.TorreHanoi.TipoStatus.Pendente);
            Assert.AreNotEqual(response.DataCriacao, new DateTime());
            Assert.AreNotEqual(response.Id, new Guid());
        }

        [TestMethod]
        [TestCategory(CategoriaTeste)]
        public void ObterTodos_Deverar_Retornar_Sucesso()
        {
            var response = _service.ObterTodos();

            Assert.IsNotNull(response);
            Assert.IsTrue(response.Count > 0);
        }
    }
}
