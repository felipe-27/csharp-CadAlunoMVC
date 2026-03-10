using System;

namespace CadAlunoMVC.Models
{
    public class ErrorViewModel
    {
        // facilitando a exibição dos erros
        public ErrorViewModel(string erro)
        {
            this.Erro = erro;
        }
        public ErrorViewModel()
        {
        }
        public string Erro { get; set; }
        public string RequestId { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
