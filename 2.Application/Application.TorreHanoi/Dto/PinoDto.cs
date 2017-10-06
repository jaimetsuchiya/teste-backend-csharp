using System.Collections.Generic;

namespace Application.TorreHanoi.Dto
{
    public class PinoDto
    {
        public PinoDto()
        {
            Discos = new List<DiscoDto>();
        }

        public string Tipo { get; set; }
        public ICollection<DiscoDto> Discos { get; set; }
    }
}
