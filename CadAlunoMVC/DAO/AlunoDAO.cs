using CadAlunoMVC.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System;

namespace CadAlunoMVC.DAO
{
    public class AlunoDAO
    {
        private SqlParameter[] CriaParametros(AlunoViewModel aluno)
        {
            SqlParameter[] parametros = new SqlParameter[5];
            parametros[0] = new SqlParameter("id", aluno.Id);
            parametros[1] = new SqlParameter("nome", aluno.Nome);

            if (aluno.Mensalidade == null)
                parametros[2] = new SqlParameter("mensalidade", DBNull.Value);
            else
                parametros[2] = new SqlParameter("mensalidade", aluno.Mensalidade);

            parametros[3] = new SqlParameter("cidadeId", aluno.CidadeId);
            parametros[4] = new SqlParameter("dataNascimento", aluno.DataNascimento);
            return parametros;
        }
        private AlunoViewModel MontaAluno(DataRow registro)
        {
            AlunoViewModel a = new AlunoViewModel();
            a.Id = Convert.ToInt32(registro["id"]);
            a.Nome = registro["nome"].ToString();
            a.CidadeId = Convert.ToInt32(registro["cidadeId"]);
            a.DataNascimento = Convert.ToDateTime(registro["dataNascimento"]);

            if (registro["mensalidade"] != DBNull.Value)
                a.Mensalidade = Convert.ToDouble(registro["mensalidade"]);

            return a;
        }
        public void Inserir(AlunoViewModel aluno)
        { /*
            string sql =
            "insert into alunos(id, nome, mensalidade, cidadeId, dataNascimento)" +
            "values ( @id, @nome, @mensalidade, @cidadeId, @dataNascimento)";
            HelperDAO.ExecutaSQL(sql, CriaParametros(aluno));
            */
            HelperDAO.ExecutaProc("spIncluiAluno", CriaParametros(aluno));
        }
        public void Alterar(AlunoViewModel aluno)
        { /*
            string sql =
            "update alunos set nome = @nome, " +
            "mensalidade = @mensalidade, " +
            "cidadeId = @cidadeId," +
            "dataNascimento = @dataNascimento where id = @id";
            HelperDAO.ExecutaSQL(sql, CriaParametros(aluno));
            */
            HelperDAO.ExecutaProc("spAlteraAluno", CriaParametros(aluno));
        }
        public void Excluir(int id)
        { /*
            string sql = "delete alunos where id =" + id;
            HelperDAO.ExecutaSQL(sql, null); 
            */

            var p = new SqlParameter[]
            {
                new SqlParameter("id", id)
            };
            HelperDAO.ExecutaProc("spExcluiAluno", p);
        }
        public AlunoViewModel Consulta(int id)
        { /*
            string sql = "select * from alunos where id = " + id;
            DataTable tabela = HelperDAO.ExecutaSelect(sql, null);
            if (tabela.Rows.Count == 0)
                return null;
            else
                return MontaAluno(tabela.Rows[0]);
            */

            var p = new SqlParameter[]
            {
                new SqlParameter("id", id)
            };
            DataTable tabela = HelperDAO.ExecutaProcSelect("spConsultaAluno", p);
            if (tabela.Rows.Count == 0)
                return null;
            else
                return MontaAluno(tabela.Rows[0]);
        }
        public List<AlunoViewModel> Listagem()
        { /*
            List<AlunoViewModel> lista = new List<AlunoViewModel>();
            string sql = "select * from alunos order by nome";
            DataTable tabela = HelperDAO.ExecutaSelect(sql, null);
            foreach (DataRow registro in tabela.Rows)
                lista.Add(MontaAluno(registro));
            return lista;
            */

            List<AlunoViewModel> lista = new List<AlunoViewModel>();
            DataTable tabela = HelperDAO.ExecutaProcSelect("spListagemAlunos", null);

            foreach (DataRow registro in tabela.Rows)
                lista.Add(MontaAluno(registro));
            return lista;
        }
    }
}
