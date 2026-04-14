using CadAlunoMVC.Models;
using System.Collections.Generic;
using System.Data;
using System;

namespace CadAlunoMVC.DAO
{
    public class CidadeDAO
    {
        public List<CidadeViewModel> ListaCidades()
        {
            List<CidadeViewModel> lista = new List<CidadeViewModel>();
            DataTable tabela = HelperDAO.ExecutaProcSelect("spListagemCidades", null);
            foreach (DataRow registro in tabela.Rows)
                lista.Add(MontaCidade(registro));
            return lista;
        }

        private CidadeViewModel MontaCidade(DataRow registro)
        {
            CidadeViewModel c = new CidadeViewModel()
            {
                Id = Convert.ToInt32(registro["id"]),
                Nome = registro["nome"].ToString()
            };
            return c;
        }
    }
}
