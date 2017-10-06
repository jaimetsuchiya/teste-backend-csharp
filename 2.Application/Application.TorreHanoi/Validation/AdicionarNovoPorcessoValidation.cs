using Application.TorreHanoi.Message;

namespace Application.TorreHanoi.Validation
{
    internal static class AdicionarNovoPorcessoValidation
    {
        internal static AdicionarNovoPorcessoResponse Validation(this int numeroDiscos)
        {
            var response = new AdicionarNovoPorcessoResponse();

            if (numeroDiscos >= 1)
            {
                return response;
            }
            response.AdicionarMensagemDeErro("É necessario ao menos um disco para executar a torre de hanoi");
            response.StatusCode = System.Net.HttpStatusCode.BadRequest;

            return response;
        }
    }
}
