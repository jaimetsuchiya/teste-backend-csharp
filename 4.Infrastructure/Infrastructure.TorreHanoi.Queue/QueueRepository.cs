using System.Collections.Generic;
using System;
using System.Linq;
using Domain.TorreHanoi.Interface.Repository;
using FluentScheduler;
using System.Threading.Tasks;
using Domain.TorreHanoi;
using Infrastructure.TorreHanoi.Cache;

namespace Infrastructure.TorreHanoi.Queue
{
    public class QueueRepository : IQueueRepository
    {
        private readonly ICacheManager _cache;
        private readonly string _cacheKey;
        private Queue<Domain.TorreHanoi.TorreHanoi> _queue;
        private readonly int _limiteProcessamento;

        public QueueRepository(ICacheManager cache)
        {
            _limiteProcessamento = 3;
            JobManager.AddJob(Processar, s => s.ToRunNow().AndEvery(10).Seconds());
            _cache = cache;
            _cacheKey = "TorreHanoi.Fila.Processamento";
        }

        private Queue<Domain.TorreHanoi.TorreHanoi> FilaProcessamento => ObterFilaProcessamento();

        public bool AdicionarNaFila(Domain.TorreHanoi.TorreHanoi torre)
        {
            FilaProcessamento.Enqueue(torre);

            _cache.DataSource = FilaProcessamento;
            _cache.Set(string.Format(_cacheKey, torre.Id));

            return true;
        }

        public Domain.TorreHanoi.TorreHanoi ObterPor(Guid id)
        {
            try
            {
                return FilaProcessamento.ToList().First(t => t.Id == id);
            }
            catch
            {
                throw new Exception("Não foi encontrado nenhum processo com o id informado");
            }
        }

        public ICollection<Domain.TorreHanoi.TorreHanoi> ObterTodos()
        {
            return FilaProcessamento.ToList();
        }

        private Queue<Domain.TorreHanoi.TorreHanoi> ObterFilaProcessamento()
        {
            return _queue ?? (_queue = (Queue<Domain.TorreHanoi.TorreHanoi>) _cache.Get(_cacheKey) ??
                                       new Queue<Domain.TorreHanoi.TorreHanoi>());
        }

        private void Processar()
        {
            var processosPendentes = FilaProcessamento.ToList().Where(t => t.Status == TipoStatus.Pendente).Take(_limiteProcessamento).ToList();

            Task.WaitAll(processosPendentes.TakeWhile(processoPendente => FilaProcessamento.Count(t => t.Status == TipoStatus.Processando) < _limiteProcessamento).Select(processoPendente => Task.Factory.StartNew(processoPendente.Processar)).ToArray());
        }
    }
}
