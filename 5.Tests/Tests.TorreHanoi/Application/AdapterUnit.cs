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
    public class AdapterUnit
    {
        private const string CategoriaTeste = "Application/Mapper/Adapter";
        private ITorreHanoiApplicationService _appService;
        private Mock<ITorreHanoiDomainService> _domainService;

        [TestInitialize]
        public void SetUp()
        {
            var mockLogger = new Mock<ILogger>();
            mockLogger.Setup(s => s.Logar(It.IsAny<string>(), It.IsAny<TipoLog>()));

            _domainService = new Mock<ITorreHanoiDomainService>();
            _domainService.Setup(s => s.Criar(It.IsAny<int>())).Returns(Guid.NewGuid);
            _domainService.Setup(s => s.ObterPor(It.IsAny<Guid>())).Returns(() => new global::Domain.TorreHanoi.TorreHanoi(3, mockLogger.Object));
            _domainService.Setup(s => s.ObterTodos()).Returns(() => new List<global::Domain.TorreHanoi.TorreHanoi> { new global::Domain.TorreHanoi.TorreHanoi(3, mockLogger.Object) });

            _appService = new TorreHanoiApplicationService(_domainService.Object, mockLogger.Object, new DesignerService());
        }

        [TestMethod]
        [TestCategory(CategoriaTeste)]
        public void DomainParaDesignerDto_Retornar_Sucesso()
        {
            var torre = _domainService.Object.ObterPor(Guid.NewGuid());
            var resultDto = new global::Application.TorreHanoi.Mapper.TorreHanoiAdapter().DomainParaDesignerDto(torre);

            Assert.IsTrue(resultDto.Id == torre.Id.ToString(), "Propriedade [Id] diferente do esperado!");

            Assert.IsTrue(resultDto.Origem.Tipo == (int)torre.Origem.Tipo, "Propriedade [Origem.Tipo] diferente do esperado!");
            Assert.IsTrue(resultDto.Origem.Discos?.Count == torre.Origem.Discos?.Count, "Propriedade [Origem.Discos] diferente do esperado!");

            Assert.IsTrue(resultDto.Intermediario.Tipo == (int)torre.Intermediario.Tipo, "Propriedade [Intermediario.Tipo] diferente do esperado!");
            Assert.IsTrue(resultDto.Intermediario.Discos?.Count == torre.Intermediario.Discos?.Count, "Propriedade [Intermediario.Discos] diferente do esperado!");

            Assert.IsTrue(resultDto.Destino.Tipo == (int)torre.Destino.Tipo, "Propriedade [Destino.Tipo] diferente do esperado!");
            Assert.IsTrue(resultDto.Destino.Discos?.Count == torre.Destino.Discos?.Count, "Propriedade [Destino.Discos] diferente do esperado!");
        }

    }
}
