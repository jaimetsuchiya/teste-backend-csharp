using Infrastructure.TorreHanoi.ServiceAgent;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.TorreHanoi.Log
{
    public class Logger : ILogger
    {
        private readonly ISlackServiceAgent _serviceAgent;
        private readonly ICollection<TipoLog> _tiposLogsDisponiveis;

        public Logger(ISlackServiceAgent serviceAgent, IEnumerable<string> tiposLogs)
        {
            _serviceAgent = serviceAgent;
            _tiposLogsDisponiveis = AdicionarLogsDisponiveis(tiposLogs);
        }

        public bool Logar(string mensgem, TipoLog tipo)
        {
            return _tiposLogsDisponiveis.Contains(tipo) && Task.Run(async () => await _serviceAgent.Post(mensgem)).Result;
        }

        private static ICollection<TipoLog> AdicionarLogsDisponiveis(IEnumerable<string> tiposLogs)
        {
            var logsDisponiveis = new List<TipoLog>();

            foreach (var tipoLog in tiposLogs)
            {
                if(Enum.TryParse(tipoLog, true, out TipoLog logDisponivel))
                {
                    logsDisponiveis.Add(logDisponivel);
                }
            }

            return logsDisponiveis;
        }
    }
}
