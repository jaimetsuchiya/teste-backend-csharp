using Application.TorreHanoi.Dto;

namespace Application.TorreHanoi.Message
{
    public class ObterProcessoPorResponse : Response
    {
        public TorreHanoiCompletaDto Processo { get; set; }
    }
}
