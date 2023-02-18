using System.Net;
using Alura.OnlineAuctions.WebApp.Data.Interfaces;
using Alura.OnlineAuctions.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace Alura.OnlineAuctions.WebApp.Controllers
{
    public class LeilaoController : Controller
    {
        private readonly IAuctionDao _auctionDao;
        private readonly ICategoryDao _categoryDao;

        public LeilaoController(IAuctionDao auctionDao, ICategoryDao categoryDao)
        {
            _auctionDao = auctionDao;
            _categoryDao = categoryDao;
        }

        public IActionResult Index()
        {
            var auctions = _auctionDao.ListAuctions();
            return View(auctions);
        }

        [HttpGet]
        public IActionResult Insert()
        {
            ViewData["Categorias"] = _categoryDao.ListCategories();
            ViewData["Operacao"] = "Inclusão";
            return View("Form");
        }

        [HttpPost]
        public IActionResult Insert(Leilao model)
        {
            if (ModelState.IsValid)
            {
                _auctionDao.InsertAuction(model);
                return RedirectToAction("Index");
            }

            ViewData["Categorias"] = _categoryDao.ListCategories();
            ViewData["Operacao"] = "Inclusão";

            return View("Form", model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewData["Categorias"] = _categoryDao.ListCategories();
            ViewData["Operacao"] = "Edição";

            var auction = _auctionDao.GetAuctionById(id);
            if (auction is null)
                return NotFound();

            return View("Form", auction);
        }

        [HttpPost]
        public IActionResult Edit(Leilao model)
        {
            if (ModelState.IsValid)
            {
                _auctionDao.UpdateAuction(model);
                return RedirectToAction("Index");
            }

            ViewData["Categorias"] = _categoryDao.ListCategories();
            ViewData["Operacao"] = "Edição";

            return View("Form", model);
        }

        [HttpPost]
        public IActionResult Inicia(int id)
        {
            var auction = _auctionDao.GetAuctionById(id);

            if (auction is null) return NotFound();
            if (auction.Situacao != SituacaoLeilao.Rascunho)
                return StatusCode((int)HttpStatusCode.MethodNotAllowed);

            auction.Situacao = SituacaoLeilao.Pregao;
            auction.Inicio = DateTime.Now;

            _auctionDao.UpdateAuction(auction);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Finaliza(int id)
        {
            var auction = _auctionDao.GetAuctionById(id);

            if (auction is null) return NotFound();
            if (auction.Situacao != SituacaoLeilao.Pregao)
                return StatusCode((int)HttpStatusCode.MethodNotAllowed);

            auction.Situacao = SituacaoLeilao.Finalizado;
            auction.Termino = DateTime.Now;

            _auctionDao.UpdateAuction(auction);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Remove(int id)
        {
            var auction = _auctionDao.GetAuctionById(id);

            if (auction is null) return NotFound();
            if (auction.Situacao == SituacaoLeilao.Pregao)
                return StatusCode((int)HttpStatusCode.MethodNotAllowed);

            _auctionDao.DeleteAuction(auction);

            return NoContent();
        }

        [HttpGet]
        public IActionResult Pesquisa(string termo)
        {
            ViewData["termo"] = termo;

            var auctions = _auctionDao
                .ListAuctions()
                .Where(l => string.IsNullOrWhiteSpace(termo) ||
                    l.Titulo.ToUpper().Contains(termo.ToUpper()) ||
                    l.Descricao.ToUpper().Contains(termo.ToUpper()) ||
                    l.Categoria.Descricao.ToUpper().Contains(termo.ToUpper())
                );

            return View("Index", auctions);
        }
    }
}
