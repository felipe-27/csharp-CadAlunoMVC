using CadAlunoMVC.DAO;
using CadAlunoMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
                AlunoDAO dao = new AlunoDAO();

                // preencher informações direto para o usuário
                aluno.Id = dao.ProximoId();
                aluno.DataNascimento = DateTime.Now;

                PreparaListaCidadesParaCombo();
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
                ValidaDados(aluno, Operacao);
                if (ModelState.IsValid == false)
                {
                    ViewBag.Operacao = Operacao;
                    PreparaListaCidadesParaCombo();
                    return View("Form", aluno);
                }
                else
                {
                    AlunoDAO dao = new AlunoDAO();
                    if (Operacao == "I")
                        dao.Inserir(aluno);
                    else
                        dao.Alterar(aluno);
                    return RedirectToAction("index");
                }
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
                PreparaListaCidadesParaCombo();

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

        private void ValidaDados(AlunoViewModel aluno, string operacao)
        {
            ModelState.Clear(); // limpa os erros criados automaticamente pelo Asp.net (que podem estar com msg em inglês)
            AlunoDAO dao = new AlunoDAO();
            if (aluno.Id <= 0)
                ModelState.AddModelError("Id", "Id inválido!");
            else
            {
                if (operacao == "I" && dao.Consulta(aluno.Id) != null)
                    ModelState.AddModelError("Id", "Código já está em uso.");
                if (operacao == "A" && dao.Consulta(aluno.Id) == null)
                    ModelState.AddModelError("Id", "Aluno não existe.");
            }
            if (string.IsNullOrEmpty(aluno.Nome))
                ModelState.AddModelError("Nome", "Preencha o nome.");
            if (aluno.Mensalidade < 0)
                ModelState.AddModelError("Mensalidade", "Campo obrigatório.");
            if (aluno.CidadeId <= 0)
                ModelState.AddModelError("CidadeId", "Informe o código da cidade.");
            if (aluno.DataNascimento > DateTime.Now)
                ModelState.AddModelError("DataNascimento", "Data inválida!");
        }

        private void PreparaListaCidadesParaCombo()
        {
            CidadeDAO cidadeDao = new CidadeDAO();
            var cidades = cidadeDao.ListaCidades();
            List<SelectListItem> listaCidades = new List<SelectListItem>();
            listaCidades.Add(new SelectListItem("Selecione uma cidade...", "0"));
            foreach (var cidade in cidades)
            {
                SelectListItem item = new SelectListItem(cidade.Nome, cidade.Id.ToString());
                listaCidades.Add(item);
            }
            ViewBag.Cidades = listaCidades;
        }
    }
}