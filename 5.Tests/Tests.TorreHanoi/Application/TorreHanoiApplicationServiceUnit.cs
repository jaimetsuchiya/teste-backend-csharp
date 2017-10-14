using System;
using System.Collections.Generic;
using System.Net;
using Application.TorreHanoi.Implementation;
using Application.TorreHanoi.Interface;
using Domain.TorreHanoi.Interface.Service;
using Infrastructure.TorreHanoi.ImagemHelper;
using Infrastructure.TorreHanoi.Log;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Tests.TorreHanoi.Application
{
    [TestClass]
    public class TorreHanoiApplicationServiceUnit
    {
        private const string CategoriaTeste = "Application/Service/TorreHanoi";

        private ITorreHanoiApplicationService _service;

        [TestInitialize]
        public void SetUp()
        {
            var mockLogger = new Mock<ILogger>();
            mockLogger.Setup(s => s.Logar(It.IsAny<string>(), It.IsAny<TipoLog>()));

            var mockTorreHanoiDomainService = new Mock<ITorreHanoiDomainService>();
            mockTorreHanoiDomainService.Setup(s => s.Criar(It.IsAny<int>())).Returns(Guid.NewGuid);
            mockTorreHanoiDomainService.Setup(s => s.ObterPor(It.IsAny<Guid>())).Returns(() => new global::Domain.TorreHanoi.TorreHanoi(3, mockLogger.Object));
            mockTorreHanoiDomainService.Setup(s => s.ObterTodos()).Returns(() => new List<global::Domain.TorreHanoi.TorreHanoi> { new global::Domain.TorreHanoi.TorreHanoi(3, mockLogger.Object) });

            _service = new TorreHanoiApplicationService(mockTorreHanoiDomainService.Object, mockLogger.Object, new DesignerService());
        }

        [TestMethod]
        [TestCategory(CategoriaTeste)]
        public void AdicionarNovoProcesso_Deve_Retornar_Sucesso()
        {
            var response = _service.AdicionarNovoPorcesso(3);

            Assert.IsNotNull(response);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Accepted);
            Assert.AreNotEqual(response.IdProcesso, new Guid());
            Assert.IsTrue(response.IsValid);
            Assert.IsTrue(response.MensagensDeErro.Count == 0);
        }

        [TestMethod]
        [TestCategory(CategoriaTeste)]
        public void ObterProcessoPor_Deverar_Retornar_Sucesso()
        {
            var response = _service.ObterProcessoPor(Guid.NewGuid().ToString());

            Assert.IsNotNull(response);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            Assert.IsNotNull(response.Processo);
            Assert.IsTrue(response.IsValid);
            Assert.IsTrue(response.MensagensDeErro.Count == 0);
        }

        [TestMethod]
        [TestCategory(CategoriaTeste)]
        public void ObterTodosProcessos_Deverar_Retornar_Sucesso()
        {
            var response = _service.ObterTodosProcessos();

            Assert.IsNotNull(response);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            Assert.IsNotNull(response.Processos);
            Assert.IsTrue(response.Processos.Count > 0);
            Assert.IsTrue(response.IsValid);
            Assert.IsTrue(response.MensagensDeErro.Count == 0);
        }


        [TestMethod]
        [TestCategory(CategoriaTeste)]
        public void ObterImagemProcessoPor_Deve_Retornar_Imagem()
        {
            var response = _service.ObterImagemProcessoPor(Guid.NewGuid().ToString("D"));
            Assert.IsTrue(response.IsValid && response.Imagem != null);
        }
    }
}
