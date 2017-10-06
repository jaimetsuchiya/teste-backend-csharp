namespace Application.TorreHanoi.Dto
{
    public class TorreHanoiResumoDto
    {
        public string Id { get; set; }
        public PinoDto Destino { get; set; }
        public PinoDto Intermediario { get; set; }
        public PinoDto Origem { get; set; }
        public string Status { get; set; }
    }
}
