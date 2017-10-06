using System.Collections.Generic;
using System.Net;

namespace Application.TorreHanoi.Message
{
    public abstract class  Response
    {
        protected Response()
        {
            MensagensDeErro = new List<string>();
            StatusCode = HttpStatusCode.OK;
        }

        public bool IsValid => ResponseIsValid();

        public ICollection<string> MensagensDeErro { get; }

        public HttpStatusCode StatusCode { get; set; }

        public void AdicionarMensagemDeErro(string mensagem)
        {
            MensagensDeErro.Add(mensagem);
        }

        private bool ResponseIsValid()
        {
            return MensagensDeErro.Count == 0;
        }
    }
}
