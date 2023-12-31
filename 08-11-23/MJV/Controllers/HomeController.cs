﻿using Microsoft.AspNetCore.Mvc;
using MJV.DALPg;
using MJV.Models;
using System.Diagnostics;

namespace MJV.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }


        [HttpGet]
        public IActionResult Index()
        {
           
            ViewBag.Autenticado = true;
            ViewData["NomeCompleto"] = "Diogo Coista Santos";
            TempData["Mensagem"] = "Mensagem teste";

            //Li esse usuario na base de dados
            Usuario usuario = new Usuario();
            usuario.Id = new Random().Next(1, 1000);
            usuario.Email = "ASdasda@gmail.com";
            usuario.Nome = "Lucius";
            usuario.SobreNome = "Pinhal Ferreira";


            usuario.Habilidades = new List<Habilidade>();
            usuario.Habilidades.Add(new Habilidade() { Nome = "C#" });
            usuario.Habilidades.Add(new Habilidade() { Nome = "html" });
            usuario.Habilidades.Add(new Habilidade() { Nome = "Css" });

            return View(usuario);
        }

        [HttpPost]
        public IActionResult ProcessaFormulario(string Nome, string SobreNome)
        {
            ViewBag.Autenticado = true;
            ViewData["NomeCompleto"] = "Diogo Coista Santos";
            TempData["Mensagem"] = "Mensagem teste";
            var u = new Usuario()
            {
                Nome = Nome,
                SobreNome = SobreNome
            };
            return View("Index", u);
        }

        [HttpPost]
        public IActionResult Index([Bind("Nome", "SobreNome")] Usuario usuario) 
        {
            ViewBag.Autenticado = true;
            ViewData["NomeCompleto"] = "Diogo Coista Santos";
            TempData["Mensagem"] = "Mensagem teste";
            var u = new Usuario()
            {
                Nome = usuario.Nome,
                SobreNome = usuario.SobreNome
            };
            return View(u); ;
        }

        public IActionResult Deletar(int id)
        {
            Console.WriteLine(id);
            //deletar banco
            return RedirectToAction("Index");
        }

        public IActionResult Deletar1(string nome)
        {
            Console.WriteLine(nome);
            //deletar banco
            return RedirectToAction("Index");
        }
        public IActionResult Privacy()
        {

            string conexao = _configuration.GetConnectionString("conexao_com_banco_postegres");
            DALPostegres sql = new DALPostegres();
            //sql.ListarAlunos();
            sql.InserirAluno("Lucius", "Pinhal", "Email@teste.com", conexao);
            List<Usuario> usuarios = new List<Usuario>();
            Usuario usuario;
            for (int i= 0; i <= 9; i++)
            {
                usuario = new Usuario();
                usuario.Email = "ASdasda@gmail.com";
                usuario.Nome = "Lucius - " + i;
                usuario.SobreNome = "Pinhal Ferreira";
                usuario.Id = i;

                usuario.Habilidades = new List<Habilidade>();
                usuario.Habilidades.Add(new Habilidade() { Nome = "C#" });
                usuario.Habilidades.Add(new Habilidade() { Nome = "html" });
                usuario.Habilidades.Add(new Habilidade() { Nome = "Css" });

                usuarios.Add(usuario);
            }
                return View(usuarios);
           
        }

        public IActionResult Mensagem()
        {
            return View("_Mensagem");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}