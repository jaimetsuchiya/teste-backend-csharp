using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain.TorreHanoi.Interface.Repository;
using Moq;
using Infrastructure.TorreHanoi.Log;
using Infrastructure.TorreHanoi.Cache;
using Infrastructure.TorreHanoi.Queue;
using System.Collections.Generic;

namespace Tests.TorreHanoi.Infrastructure
{
    [TestClass]
    public class QueueUnit
    {
        private const string CategoriaTeste = "Infrastructure/Repository/Queue";

        private IQueueRepository _repository;
        private Mock<ILogger> _mockLogger;
        private global::Domain.TorreHanoi.TorreHanoi _torre;

        [TestInitialize]
        public void SetUp()
        {
            _mockLogger = new Mock<ILogger>();
            _mockLogger.Setup(s => s.Logar(It.IsAny<string>(), It.IsAny<TipoLog>()));

            _torre = new global::Domain.TorreHanoi.TorreHanoi(3, _mockLogger.Object);

            var mockCacheManager = new Mock<ICacheManager>();
            mockCacheManager.Setup(s => s.Set(It.IsAny<string>()));
            mockCacheManager.Setup(s => s.Get(It.IsAny<string>())).Returns(CriarMockGetCacheManager);

            _repository = new QueueRepository(mockCacheManager.Object);
        }

        [TestMethod]
        [TestCategory(CategoriaTeste)]
        public void AdicionarNaFila_Deve_Retornar_Sucesso()
        {
            var result = _repository.AdicionarNaFila(new global::Domain.TorreHanoi.TorreHanoi(3, _mockLogger.Object));

            Assert.IsTrue(result);
        }

        [TestMethod]
        [TestCategory(CategoriaTeste)]
        public void ObterPor_Deve_Retornar_Sucesso()
        {
            var result = _repository.ObterPor(_torre.Id);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        [TestCategory(CategoriaTeste)]
        [ExpectedException(typeof(Exception), "Não é possivel obter um item que nao existe")]
        public void ObterPor_Deve_Retornar_Erro()
        {
            var result = _repository.ObterPor(Guid.NewGuid());

            Assert.IsNull(result);
        }

        [TestMethod]
        [TestCategory(CategoriaTeste)]
        public void ObterTodos_Deve_Retornar_Sucesso()
        {
            var result = _repository.ObterTodos();

            Assert.IsNotNull(result);
        }

        private Queue<global::Domain.TorreHanoi.TorreHanoi> CriarMockGetCacheManager()
        {
            var fila = new Queue<global::Domain.TorreHanoi.TorreHanoi>();

            fila.Enqueue(_torre);

            return fila;
        }
    }
}
