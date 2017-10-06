using Application.TorreHanoi.Message;
using System;

namespace Application.TorreHanoi.Validation
{
    internal static class ObterProcessoPorValidation
    {
        internal static ObterProcessoPorResponse ValidationProcesso(this string id)
        {
            var response = new ObterProcessoPorResponse();

            if (Guid.TryParse(id, out var _))
            {
                return response;
            }
            response.AdicionarMensagemDeErro($"É o id {id} não esta em um formato valido");
            response.StatusCode = System.Net.HttpStatusCode.BadRequest;

            return response;
        }
    }
}
