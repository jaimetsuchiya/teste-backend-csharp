using Application.TorreHanoi.Interface;
using Application.TorreHanoi.Mapper;
using Application.TorreHanoi.Message;
using Application.TorreHanoi.Validation;
using Domain.TorreHanoi.Interface.Service;
using Infrastructure.TorreHanoi.ImagemHelper;
using Infrastructure.TorreHanoi.Log;
using System;

namespace Application.TorreHanoi.Implementation
{
    public class TorreHanoiApplicationService : ITorreHanoiApplicationService
    {
        private readonly TorreHanoiAdapter _adpterTorreHanoi;
        private readonly ITorreHanoiDomainService _domainService;
        private readonly ILogger _log;
        private readonly IDesignerService _designerService;

        public TorreHanoiApplicationService(ITorreHanoiDomainService domainService, ILogger log, IDesignerService designerService)
        {
            _domainService = domainService;
            _adpterTorreHanoi = new TorreHanoiAdapter();
            _log = log;
            _designerService = designerService;
        }

        public AdicionarNovoPorcessoResponse AdicionarNovoPorcesso(int numeroDiscos)
        {
            var response = numeroDiscos.Validation();

            if (!response.IsValid)
            {
                return response;
            }

            try
            {
                response.IdProcesso = _domainService.Criar(numeroDiscos);
                response.StatusCode = System.Net.HttpStatusCode.Accepted;
            }
            catch (Exception ex)
            {
                _log.Logar($"Ocorreu um erro ao criar um processo com {numeroDiscos} numero de discos :{ex.Message}", TipoLog.Erro);
                response.AdicionarMensagemDeErro("Ocorreu um erro ao criar um processo");
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
            }

            return response;
        }

        public ObterProcessoPorResponse ObterProcessoPor(string id)
        {
            var response = id.ValidationProcesso();

            if (!response.IsValid)
            {
                return response;
            }

            try
            {
                response.Processo = _adpterTorreHanoi.DomainParaApplicationDto(_domainService.ObterPor(new Guid(id)));
            }
            catch (Exception ex)
            {
                _log.Logar($"Ocorreu um erro ao obter o processo pelo id {id} : {ex.Message}", TipoLog.Erro);
                response.AdicionarMensagemDeErro($"Ocorreu o erro ao obter um processo pelo id {id}");
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
            }

            return response;
        }

        public ObterTodosProcessosResponse ObterTodosProcessos()
        {
            var response = new ObterTodosProcessosResponse();

            if (!response.IsValid)
            {
                return response;
            }
            try
            {
                response.Processos = _adpterTorreHanoi.DomainParaApplicationDto(_domainService.ObterTodos());
            }
            catch (Exception ex)
            {
                _log.Logar($"Ocorreu um erro ao listar todos os processos : {ex.Message}", TipoLog.Erro);
                response.AdicionarMensagemDeErro("Ocorreu um erro ao listar todos os processos");
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
            }

            return response;
        }

        public ObterImagemProcessoPorResponse ObterImagemProcessoPor(string id)
        {
            var response = id.ValidationImagem();

            if (!response.IsValid)
            {
                return response;
            }
            try
            {
                var torre = _domainService.ObterPor(Guid.Parse(id));

                _designerService.Inicializar(_adpterTorreHanoi.DomainParaDesignerDto(torre));

                response.Imagem = _designerService.Desenhar();
            }
            catch (Exception ex)
            {
                _log.Logar($"Ocorreu um erro ao obter a imagem pelo id {id} : {ex.Message}", TipoLog.Erro);
                response.AdicionarMensagemDeErro($"Ocorreu um erro ao obter a imagem pelo id {id}");
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
            }

            return response;
        }
    }
}
