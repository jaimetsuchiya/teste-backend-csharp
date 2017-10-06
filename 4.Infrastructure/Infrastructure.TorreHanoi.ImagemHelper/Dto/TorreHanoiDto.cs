using System.Collections.Generic;

namespace Infrastructure.TorreHanoi.ImagemHelper.Dto
{
    public class TorreHanoiDto
    {
        public TorreHanoiDto()
        {
            Discos = new List<DiscoDto>();
        }

        public string Id { get; set; }
        public PinoDto Destino { get; set; }
        public PinoDto Intermediario { get; set; }
        public PinoDto Origem { get; set; }
        public ICollection<DiscoDto> Discos { get; set; }
    }
}
