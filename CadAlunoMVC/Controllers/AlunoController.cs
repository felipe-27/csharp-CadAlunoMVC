using CadAlunoMVC.DAO;
using CadAlunoMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace CadAlunoMVC.Controllers
{
    public class AlunoController : Controller
    {
        // método importante para interagir com o usuário
        public IActionResult Index()
        {
            AlunoDAO dao = new AlunoDAO();
            List<AlunoViewModel> lista = dao.Listagem();
            return View(lista);
        }

        public IActionResult Create()
        {
            AlunoViewModel aluno = new AlunoViewModel();

            // preencher informações direto para o usuário
            aluno.DataNascimento = DateTime.Now;
            return View("Form", aluno);
        }

        public IActionResult Salvar(AlunoViewModel aluno)
        {
            AlunoDAO dao = new AlunoDAO();
            dao.Inserir(aluno);
            return RedirectToAction("index");
        }
    }
}