using CadAlunoMVC.DAO;
using CadAlunoMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;

namespace CadAlunoMVC.Controllers
{
    public class AlunoController : Controller
    {
        // método importante para interagir com o usuário
        public IActionResult Index()
        {
            try
            {
                AlunoDAO dao = new AlunoDAO();
                List<AlunoViewModel> lista = dao.Listagem();
                return View(lista);
            }
            catch (Exception ex)
            {
                // mesmo estando fora da mesma pasta com o nome da controller,
                // a View consegue encontrar 
                return View("Error", new ErrorViewModel(ex.ToString()));
            }
            
        }

        public IActionResult Create()
        {
            try
            {
                ViewBag.Operacao = "I";
                AlunoViewModel aluno = new AlunoViewModel();

                // preencher informações direto para o usuário
                aluno.DataNascimento = DateTime.Now;
                return View("Form", aluno);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel(ex.ToString()));
            }
        }

        public IActionResult Salvar(AlunoViewModel aluno, string Operacao)
        {
            try
            {
                AlunoDAO dao = new AlunoDAO();

                if (Operacao == "I")
                    dao.Inserir(aluno);
                else
                    dao.Alterar(aluno);

                return RedirectToAction("index");
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel(ex.ToString()));
            }
        }

        public IActionResult Edit(int id)
        {
            try
            {
                ViewBag.Operacao = "A";
                AlunoDAO dao = new AlunoDAO();
                AlunoViewModel aluno = dao.Consulta(id); // busca o aluno no banco de dados

                if (aluno == null)
                    return RedirectToAction("index");
                else
                    return View("Form", aluno);
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }

        public IActionResult Delete(int id)
        {
            try
            {
                AlunoDAO dao = new AlunoDAO();
                dao.Excluir(id);
                return RedirectToAction("index");
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }
    }
}