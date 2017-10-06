using System.Collections.Generic;

namespace Infrastructure.TorreHanoi.ImagemHelper.Dto
{
    public class PinoDto
    {
        public PinoDto()
        {
            Discos = new List<DiscoDto>();
        }

        public int Tipo { get; set; }
        public ICollection<DiscoDto> Discos { get; set; }
    }
}
