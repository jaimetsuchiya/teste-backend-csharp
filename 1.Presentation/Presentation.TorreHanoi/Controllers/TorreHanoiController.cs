using Application.TorreHanoi.Interface;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace Presentation.TorreHanoi.Controllers
{
    public class TorreHanoiController : ApiController
    {
        private readonly ITorreHanoiApplicationService _service;

        public TorreHanoiController(ITorreHanoiApplicationService service)
        {
            _service = service;
        }

        /// <summary>
        /// Adiciona um processo da torre de hanoi na fila para ser processado de forma assíncrona
        /// </summary>
        /// <param name="numeroDiscos">Numero de discos</param>
        /// <returns>Retorna o id do novo processo, utilizado para consultar o andamento do processo</returns>
        [HttpPost]
        [Route("Api/Torre/Hanoi/{numeroDiscos}")]
        public HttpResponseMessage Post(int numeroDiscos)
        {
            var response = _service.AdicionarNovoPorcesso(numeroDiscos);

            return Request.CreateResponse(response.StatusCode, response);
        }

        /// <summary>
        /// Obtem o estado atual de um determinado processo
        /// </summary>
        /// <param name="id">Numero do processo que deseja consultar</param>
        /// <returns>Snapshot atual do processo</returns>
        [HttpGet]
        [Route("Api/Torre/Hanoi/{id}")]
        public HttpResponseMessage Get(string id)
        {
            var response = _service.ObterProcessoPor(id);

            return Request.CreateResponse(response.StatusCode, response);
        }

        /// <summary>
        /// Obtem a imagem atual de um determinado processo
        /// </summary>
        /// <param name="id">Numero do processo que deseja consultar</param>
        /// <returns>imagem atual do processo</returns>
        [HttpGet]
        [Route("Api/Torre/Hanoi/Imagem/{id}")]
        public HttpResponseMessage GetImagem(string id)
        {
            var response = _service.ObterImagemProcessoPor(id);
            var httpResponseMessage = new HttpResponseMessage();
            var memoryStream = new MemoryStream();
            response.Imagem?.Save(memoryStream, ImageFormat.Bmp);
            memoryStream.Position = 0;
            httpResponseMessage.Content = new StreamContent(memoryStream);
            httpResponseMessage.Content.Headers.ContentLength = memoryStream.Length;
            httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("image/Bmp");
            httpResponseMessage.StatusCode = response.StatusCode;

            return httpResponseMessage;
        }

        /// <summary>
        /// Obtem uma lista com todos os processos
        /// </summary>
        /// <returns>Snapshot atual de todos os processos</returns>
        [HttpGet]
        [Route("Api/Torre/Hanoi")]
        public HttpResponseMessage Get()
        {
            var response = _service.ObterTodosProcessos();

            return Request.CreateResponse(response.StatusCode, response);
        }
    }
}
