﻿using System;
using Infrastructure.TorreHanoi.Log;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Tests.TorreHanoi.Domain
{
    [TestClass]
    public class TorreHanoiUnit
    {
        private const string CategoriaTeste = "Domain/TorreHanoi";

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
            var torre = new global::Domain.TorreHanoi.TorreHanoi(4, _mockLogger.Object);
            Assert.IsTrue(torre.Id != Guid.Empty);
        }

        [TestMethod]
        [TestCategory(CategoriaTeste)]
        public void Processar_Deverar_Retornar_Sucesso()
        {
            var torre = new global::Domain.TorreHanoi.TorreHanoi(4, _mockLogger.Object);
            torre.Processar();

            Assert.IsTrue(torre.Status == global::Domain.TorreHanoi.TipoStatus.FinalizadoSucesso);
        }


        [TestMethod]
        [TestCategory(CategoriaTeste)]
        public void Processar_Deverar_Retornar_Erro()
        {
            var torre = new global::Domain.TorreHanoi.TorreHanoi(0, _mockLogger.Object);
            torre.Processar();

            Assert.IsTrue(torre.Status == global::Domain.TorreHanoi.TipoStatus.FinalizadoErro);
        }
    }
}
