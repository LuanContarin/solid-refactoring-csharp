using Alura.OnlineAuctions.WebApp.Data.Interfaces;
using Alura.OnlineAuctions.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace Alura.OnlineAuctions.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICategoryDao _categoryDao;
        private readonly IAuctionDao _auctionDao;

        public HomeController(ICategoryDao categoryDao, IAuctionDao auctionDao)
        {
            _categoryDao = categoryDao;
            _auctionDao = auctionDao;
        }

        public IActionResult Index()
        {
            var categories = _categoryDao
                .ListCategoriesWithAuctions()
                .Select(c => new CategoryWithInfoAuction
                {
                    Id = c.Id,
                    Description = c.Description,
                    Image = c.Image,
                    InDraft = c.Auctions.Where(l => l.Status == AuctionStatus.Draft).Count(),
                    InFloor = c.Auctions.Where(l => l.Status == AuctionStatus.Floor).Count(),
                    Finalized = c.Auctions.Where(l => l.Status == AuctionStatus.Finished).Count(),
                });

            return View(categories);
        }

        [Route("[controller]/StatusCode/{statusCode}")]
        public IActionResult StatusCodeError(int statusCode)
        {
            if (statusCode == 404) return View("404");
            return View(statusCode);
        }

        [Route("[controller]/Category/{idCategory}")]
        public IActionResult Category(int idCategory)
        {
            var categories = _categoryDao
                .ListCategoriesWithAuctions()
                .First(c => c.Id == idCategory);

            return View(categories);
        }

        [HttpPost]
        [Route("[controller]/Search")]
        public IActionResult Search(string search)
        {
            ViewData["search"] = search;

            var searchNormalized = search.ToUpper();
            var auctions = _auctionDao
                .ListAuctions()
                .Where(c =>
                    c.Title.ToUpper().Contains(searchNormalized) ||
                    c.Description.ToUpper().Contains(searchNormalized) ||
                    c.Category.Description.ToUpper().Contains(searchNormalized));

            return View(auctions);
        }
    }
}
