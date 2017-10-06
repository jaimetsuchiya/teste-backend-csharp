using Domain.TorreHanoi.Interface.Repository;
using Domain.TorreHanoi.Interface.Service;
using Infrastructure.TorreHanoi.Log;
using System;
using System.Collections.Generic;

namespace Domain.TorreHanoi.Service
{
    public class TorreHanoiDomainService : ITorreHanoiDomainService
    {
        private readonly IQueueRepository _queueRepository;
        private readonly ILogger _log;

        public TorreHanoiDomainService(IQueueRepository queueRepository, ILogger log)
        {
            _queueRepository = queueRepository;
            _log = log;
        }

        public Guid Criar(int numeroDiscos)
        {
            var torreHanoi = new TorreHanoi(numeroDiscos, _log);

            PublicarNaFila(torreHanoi);

            return torreHanoi.Id;
        }

        public TorreHanoi ObterPor(Guid id)
        {
            return _queueRepository.ObterPor(id);
        }

        public ICollection<TorreHanoi> ObterTodos()
        {
            return _queueRepository.ObterTodos();
        }

        private void PublicarNaFila(TorreHanoi torreHanoi)
        {
            _log.Logar($"TorreHanoi id { torreHanoi.Id} -> Adicionando na fila", TipoLog.Fluxo);
            _queueRepository.AdicionarNaFila(torreHanoi);
        }
    }
}
