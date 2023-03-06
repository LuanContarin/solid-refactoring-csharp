using Alura.OnlineAuctions.WebApp.Models;
using Alura.OnlineAuctions.WebApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Alura.OnlineAuctions.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _productService;

        public HomeController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index()
        {
            var categories = _productService.ListCategoriesWithAuctions();

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
            var category = _productService.GetCategoryWithAuctionsById(idCategory);

            return View(category);
        }

        [HttpPost]
        [Route("[controller]/Search")]
        public IActionResult Search(string search)
        {
            ViewData["search"] = search;

            var searchNormalized = search.ToUpper();
            var auctions = _productService.ListAuctionsBySearch(searchNormalized);

            return View(auctions);
        }
    }
}
