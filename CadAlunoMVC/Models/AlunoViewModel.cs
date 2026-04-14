using System;

namespace CadAlunoMVC.Models
{
    public class AlunoViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        // "double?" ==> "nullable" value
        public double? Mensalidade { get; set; }
        public int CidadeId { get; set; }
        public DateTime DataNascimento { get; set; }

        // <summary>
        /// Campo para registar o nome da cidade após o join. Não é salvo na tabela Aluno
        /// </summary>
        public string NomeCidade { get; set; }
    }
}
