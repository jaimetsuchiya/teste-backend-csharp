using Application.TorreHanoi.Dto;
using System.Collections.Generic;

namespace Application.TorreHanoi.Message
{
    public class ObterTodosProcessosResponse : Response
    {
        public ICollection<TorreHanoiResumoDto> Processos { get; set; }
    }
}
