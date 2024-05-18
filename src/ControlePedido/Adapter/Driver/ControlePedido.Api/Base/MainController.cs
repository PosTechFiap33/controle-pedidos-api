using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ControlePedido.Api.Base
{
    public abstract class MainController : ControllerBase
	{
        protected ICollection<string> Erros = new List<string>();

        protected ActionResult CustomResponse(object result = null)
        {
            if (OperacaoValida())
                return Ok(result);

            return BadRequest(RecuperarErros());
        }

        protected bool OperacaoValida()
        {
            return !Erros.Any();
        }

        private ValidationProblemDetails RecuperarErros()
        {
            return new ValidationProblemDetails(new Dictionary<string, string[]> {
                {
                    "Mensagens", Erros.ToArray()
                }
            });
        }

        protected void AdicionarErroProcessamento(string erro)
        {
            Erros.Add(erro);
        }

        protected void LimparErrosProcessamento()
        {
            Erros.Clear();
        }
    }
}



