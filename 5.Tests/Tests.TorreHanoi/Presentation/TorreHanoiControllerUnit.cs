using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Hosting;
using Application.TorreHanoi.Dto;
using Application.TorreHanoi.Interface;
using Application.TorreHanoi.Message;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using Presentation.TorreHanoi.Controllers;

namespace Tests.TorreHanoi.Presentation
{
    [TestClass]
    public class TorreHanoiControllerUnit
    {
        private const string CategoriaTeste = "Presentation/Controller/TorreHanoi";

        private TorreHanoiController _controller;
        private readonly string _idProcesso = Guid.NewGuid().ToString();

        [TestInitialize]
        public void SetUp()
        {
            var mockAdicionarNovoPorcessoResponse = new AdicionarNovoPorcessoResponse { IdProcesso = Guid.NewGuid(), StatusCode = HttpStatusCode.Accepted };
            var mockObterProcessoPorResponse = new ObterProcessoPorResponse { StatusCode = HttpStatusCode.OK, Processo = CriarMockTorreHanoiCompletaDto() };
            var mockObterTodosProcessosResponse = new ObterTodosProcessosResponse { StatusCode = HttpStatusCode.OK, Processos = CriarMockTorreHanoiResumoDto() };

            var mockTorreHanoiApplicationService = new Mock<ITorreHanoiApplicationService>();
            mockTorreHanoiApplicationService.Setup(s => s.AdicionarNovoPorcesso(It.IsAny<int>())).Returns(() => mockAdicionarNovoPorcessoResponse);
            mockTorreHanoiApplicationService.Setup(s => s.ObterProcessoPor(It.Is<string>(id => id.Equals(_idProcesso)))).Returns(() => mockObterProcessoPorResponse);
            mockTorreHanoiApplicationService.Setup(s => s.ObterTodosProcessos()).Returns(() => mockObterTodosProcessosResponse);

            _controller =
                new TorreHanoiController(mockTorreHanoiApplicationService.Object) {Request = new HttpRequestMessage()};
            _controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
        }

        [TestMethod]
        [TestCategory(CategoriaTeste)]
        public void AdicionarNovoProcesso_Deve_Retornar_Sucesso()
        {
            var httpResponse = _controller.Post(3);

            var response = JsonConvert.DeserializeObject<AdicionarNovoPorcessoResponse>(httpResponse.Content.ReadAsStringAsync().Result);

            Assert.IsNotNull(httpResponse);
            Assert.AreEqual(httpResponse.StatusCode, HttpStatusCode.Accepted);
            Assert.AreNotEqual(response.IdProcesso, new Guid());
            Assert.IsTrue(response.IsValid);
            Assert.IsTrue(response.MensagensDeErro.Count == 0);
        }

        [TestMethod]
        [TestCategory(CategoriaTeste)]
        public void ObterProcessoPor_Deverar_Retornar_Sucesso()
        {
            var httpResponse = _controller.Get(_idProcesso);

            var response = JsonConvert.DeserializeObject<ObterProcessoPorResponse>(httpResponse.Content.ReadAsStringAsync().Result);

            Assert.IsNotNull(httpResponse);
            Assert.AreEqual(httpResponse.StatusCode, HttpStatusCode.OK);
            Assert.IsNotNull(response.Processo);
            Assert.AreEqual(response.Processo.Id, _idProcesso);
            Assert.IsTrue(response.IsValid);
            Assert.IsTrue(response.MensagensDeErro.Count == 0);
        }

        [TestMethod]
        [TestCategory(CategoriaTeste)]
        public void ObterTodosProcessos_Deverar_Retornar_Sucesso()
        {
            var httpResponse = _controller.Get();

            var response = JsonConvert.DeserializeObject<ObterTodosProcessosResponse>(httpResponse.Content.ReadAsStringAsync().Result);

            Assert.IsNotNull(httpResponse);
            Assert.AreEqual(httpResponse.StatusCode, HttpStatusCode.OK);
            Assert.IsNotNull(response.Processos);
            Assert.IsTrue(response.Processos.Count > 0);
            Assert.IsTrue(response.IsValid);
            Assert.IsTrue(response.MensagensDeErro.Count == 0);
        }

        private TorreHanoiCompletaDto CriarMockTorreHanoiCompletaDto()
        {
            var torre = new TorreHanoiCompletaDto
            {
                Id = _idProcesso,
                Status = "Pedente",
                DataCriacao = DateTime.Now,
                Origem = new PinoDto
                {
                    Discos = new List<DiscoDto>
                    {
                        new DiscoDto {Id = 1},
                        new DiscoDto {Id = 2},
                        new DiscoDto {Id = 3}
                    },
                    Tipo = "Origem"
                },
                Destino = new PinoDto {Discos = new List<DiscoDto> {new DiscoDto()}, Tipo = "Destino"},
                Intermediario = new PinoDto {Discos = new List<DiscoDto> {new DiscoDto()}, Tipo = "Intermediario"}
            };

            return torre;
        }

        private List<TorreHanoiResumoDto> CriarMockTorreHanoiResumoDto()
        {
            var torres = new List<TorreHanoiResumoDto>();

            var torre = new TorreHanoiResumoDto
            {
                Id = _idProcesso,
                Status = "Pedente",
                Origem = new PinoDto
                {
                    Discos = new List<DiscoDto> {new DiscoDto {Id = 1}, new DiscoDto {Id = 2}, new DiscoDto {Id = 3}},
                    Tipo = "Origem"
                },
                Destino = new PinoDto {Discos = new List<DiscoDto> {new DiscoDto()}, Tipo = "Destino"},
                Intermediario = new PinoDto {Discos = new List<DiscoDto> {new DiscoDto()}, Tipo = "Intermediario"}
            };

            torres.Add(torre);
            return torres;
        }
    }
}
