using Infrastructure.TorreHanoi.Log;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Domain.TorreHanoi
{
    public class TorreHanoi
    {
        private readonly ILogger _log;

        public TorreHanoi(int numeroDiscos, ILogger log)
        {
            _log = log;

            Id = Guid.NewGuid();
            Discos = CadastrarDiscos(numeroDiscos);
            Destino = new Pino("Destino", new List<Disco>());
            Intermediario = new Pino("Intermediario", new List<Disco>());
            Origem = new Pino("Origem", new List<Disco>(Discos));
            DataCriacao = DateTime.Now;
            Status = TipoStatus.Pendente;
            PassoAPasso = new List<string>();
        }

        public Guid Id { get; }
        public ICollection<Disco> Discos { get; }
        public Pino Destino { get; }
        public Pino Intermediario { get; }
        public Pino Origem { get; }
        public DateTime DataCriacao { get; }
        public DateTime? DataFinalizacao { get; private set; }
        public TipoStatus Status { get; private set; }
        public ICollection<string> PassoAPasso { get; }

        public void Processar()
        {
            Status = TipoStatus.Processando;
            _log.Logar($"TorreHanoi id {Id} -> Iniciando Processamento", TipoLog.Fluxo);
            try
            {
                if (Discos.Count <= 0)
                {
                    Status = TipoStatus.FinalizadoErro;
                    _log.Logar($"TorreHanoi id {Id} -> Processo finalizado com erro, número de discos inválido:{Discos.Count}", TipoLog.Fluxo);
                }
                else
                {
                    Resolver(Discos.Count, Origem, Intermediario, Destino);

                    Status = TipoStatus.FinalizadoSucesso;
                    _log.Logar($"TorreHanoi id {Id} -> Processo finalizado com sucesso", TipoLog.Fluxo);
                }
            }
            catch(Exception ex)
            {
                Status = TipoStatus.FinalizadoErro;
                _log.Logar($"TorreHanoi id {Id} -> Ocorreu um erro ao finalizar o processo. Ex: {ex.Message}", TipoLog.Fluxo);
            }
            finally
            {
                DataFinalizacao = DateTime.Now;
            }
        }

        private void Resolver(int numeroDiscosRestante, Pino origem, Pino intermediario, Pino destino)
        {
            _log.Logar($"Resolver Parametros = numeroDiscosRestantes:{numeroDiscosRestante}, origem:{origem.Tipo.ToString()}, intermediario: {intermediario.Tipo.ToString()}, destino: {destino.Tipo.ToString()}", TipoLog.Fluxo);

            if (numeroDiscosRestante == 1)
            {
                MoverDisco(origem, destino);
            }
            else
            {
                Resolver(numeroDiscosRestante - 1, origem, destino, intermediario);
                MoverDisco(origem, destino);
                Resolver(numeroDiscosRestante - 1, intermediario, origem, destino);
            }
        }

        private void MoverDisco(Pino pinoInicio, Pino pinoFim)
        {
            Thread.Sleep(1000);
            var disco = pinoInicio.RemoverDisco();
            pinoFim.AdicionarDisco(disco);
            PassoAPasso.Add($"Movendo disco {disco.Id} do pino {pinoInicio.Tipo}, para o pino {pinoFim.Tipo}");

            _log.Logar($"TorreHanoi id {Id} -> Movendo disco {disco.Id} do pino {pinoInicio.Tipo}, para o pino {pinoFim.Tipo}", TipoLog.Info);
        }

        private static ICollection<Disco> CadastrarDiscos(int numeroDiscos)
        {
            var discos = new List<Disco>();

            for (var i = numeroDiscos; i >= 1; i--)
            {
                discos.Add(new Disco(i));
            }

            return discos;
        }
    }
}
