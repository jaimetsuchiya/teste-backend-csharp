using System;
using System.Collections.Generic;

namespace Application.TorreHanoi.Dto
{
    public class TorreHanoiCompletaDto
    {
        public TorreHanoiCompletaDto()
        {
            PassoAPasso = new List<string>();
        }

        public string Id { get; set; }
        public PinoDto Destino { get; set; }
        public PinoDto Intermediario { get; set; }
        public PinoDto Origem { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataFinalizacao { get; set; }
        public string Status { get; set; }
        public ICollection<string> PassoAPasso { get; set; }
    }
}
