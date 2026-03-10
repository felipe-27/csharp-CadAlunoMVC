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

        public IActionResult Salvar(AlunoViewModel aluno)
        {
            try
            {
                AlunoDAO dao = new AlunoDAO();
                dao.Inserir(aluno);
                return RedirectToAction("index");
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel(ex.ToString()));
            }
        }
    }
}