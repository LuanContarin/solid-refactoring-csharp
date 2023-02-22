using System.Net;
using Alura.OnlineAuctions.WebApp.Data.Interfaces;
using Alura.OnlineAuctions.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace Alura.OnlineAuctions.WebApp.Controllers
{
    public class AuctionController : Controller
    {
        private readonly IAuctionDao _auctionDao;
        private readonly ICategoryDao _categoryDao;

        public AuctionController(IAuctionDao auctionDao, ICategoryDao categoryDao)
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
            ViewData["Categories"] = _categoryDao.ListCategories();
            ViewData["Operation"] = "Insert";
            return View("Form");
        }

        [HttpPost]
        public IActionResult Insert(Auction model)
        {
            if (ModelState.IsValid)
            {
                _auctionDao.InsertAuction(model);
                return RedirectToAction("Index");
            }

            ViewData["Categories"] = _categoryDao.ListCategories();
            ViewData["Operation"] = "Insert";

            return View("Form", model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewData["Categories"] = _categoryDao.ListCategories();
            ViewData["Operation"] = "Update";

            var auction = _auctionDao.GetAuctionById(id);
            if (auction is null)
                return NotFound();

            return View("Form", auction);
        }

        [HttpPost]
        public IActionResult Update(Auction model)
        {
            if (ModelState.IsValid)
            {
                _auctionDao.UpdateAuction(model);
                return RedirectToAction("Index");
            }

            ViewData["Categories"] = _categoryDao.ListCategories();
            ViewData["Operation"] = "Update";

            return View("Form", model);
        }

        [HttpPost]
        public IActionResult Start(int id)
        {
            var auction = _auctionDao.GetAuctionById(id);

            if (auction is null) return NotFound();
            if (auction.Status != AuctionStatus.Draft)
                return StatusCode(StatusCodes.Status405MethodNotAllowed);

            auction.Status = AuctionStatus.Floor;
            auction.Start = DateTime.Now;

            _auctionDao.UpdateAuction(auction);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Finish(int id)
        {
            var auction = _auctionDao.GetAuctionById(id);

            if (auction is null) return NotFound();
            if (auction.Status != AuctionStatus.Floor)
                return StatusCode(StatusCodes.Status405MethodNotAllowed);

            auction.Status = AuctionStatus.Finished;
            auction.End = DateTime.Now;

            _auctionDao.UpdateAuction(auction);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Remove(int id)
        {
            var auction = _auctionDao.GetAuctionById(id);

            if (auction is null) return NotFound();
            if (auction.Status == AuctionStatus.Floor)
                return StatusCode(StatusCodes.Status405MethodNotAllowed);

            _auctionDao.DeleteAuction(auction);

            return NoContent();
        }

        [HttpGet]
        public IActionResult Search(string search)
        {
            ViewData["search"] = search;

            var auctions = _auctionDao
                .ListAuctions()
                .Where(l => string.IsNullOrWhiteSpace(search) ||
                    l.Title.ToUpper().Contains(search.ToUpper()) ||
                    l.Description.ToUpper().Contains(search.ToUpper()) ||
                    l.Category.Description.ToUpper().Contains(search.ToUpper())
                );

            return View("Index", auctions);
        }
    }
}
