using Domain.TorreHanoi.Interface.Service;
using System.Drawing;
using Infrastructure.TorreHanoi.Log;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using Infrastructure.TorreHanoi.ImagemHelper;

namespace Tests.TorreHanoi.Infrastructure
{
    [TestClass]
    public class DesignUnit
    {
        private const string CategoriaTeste = "Infrastructure/ImagemHelper/Designer";
        private Mock<ITorreHanoiDomainService> _domainService;
        private IDesignerService _designerService;

        [TestInitialize]
        public void SetUp()
        {
            var mockLogger = new Mock<ILogger>();
            mockLogger.Setup(s => s.Logar(It.IsAny<string>(), It.IsAny<TipoLog>()));

            _domainService = new Mock<ITorreHanoiDomainService>();
            _domainService.Setup(s => s.Criar(It.IsAny<int>())).Returns(Guid.NewGuid);
            _domainService.Setup(s => s.ObterPor(It.IsAny<Guid>())).Returns(() => new global::Domain.TorreHanoi.TorreHanoi(3, mockLogger.Object));
            _domainService.Setup(s => s.ObterTodos()).Returns(() => new List<global::Domain.TorreHanoi.TorreHanoi> { new global::Domain.TorreHanoi.TorreHanoi(3, mockLogger.Object) });

            _designerService = new DesignerService();
        }


        [TestMethod]
        [TestCategory(CategoriaTeste)]
        public void Desenhar_Deve_Retornar_Sucesso()
        {
            var torre = _domainService.Object.ObterPor(Guid.NewGuid());
            var resultDto = new global::Application.TorreHanoi.Mapper.TorreHanoiAdapter().DomainParaDesignerDto(torre);
            _designerService.Inicializar(resultDto);

            var imagem = _designerService.Desenhar();

            Assert.IsTrue(imagem != null, "A Imagem não foi gerada!");
        }
    }
}
