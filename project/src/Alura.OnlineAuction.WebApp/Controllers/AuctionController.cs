using Alura.OnlineAuctions.WebApp.Models;
using Alura.OnlineAuctions.WebApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Alura.OnlineAuctions.WebApp.Controllers
{
    public class AuctionController : Controller
    {
        private readonly IAdminService _adminService;

        public AuctionController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public IActionResult Index()
        {
            var auctions = _adminService.ListAuctions();
            return View(auctions);
        }

        [HttpGet]
        public IActionResult Insert()
        {
            ViewData["Categories"] = _adminService.ListCategories();
            ViewData["Operation"] = "Insert";
            return View("Form");
        }

        [HttpPost]
        public IActionResult Insert(Auction model)
        {
            if (ModelState.IsValid)
            {
                _adminService.InsertAuction(model);
                return RedirectToAction("Index");
            }

            ViewData["Categories"] = _adminService.ListCategories();
            ViewData["Operation"] = "Insert";

            return View("Form", model);
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            ViewData["Categories"] = _adminService.ListCategories();
            ViewData["Operation"] = "Update";

            var auction = _adminService.GetAuctionById(id);
            if (auction is null)
                return NotFound();

            return View("Form", auction);
        }

        [HttpPost]
        public IActionResult Update(Auction model)
        {
            if (ModelState.IsValid)
            {
                _adminService.UpdateAuction(model);
                return RedirectToAction("Index");
            }

            ViewData["Categories"] = _adminService.ListCategories();
            ViewData["Operation"] = "Update";

            return View("Form", model);
        }

        [HttpPost]
        public IActionResult Start(int id)
        {
            var auction = _adminService.GetAuctionById(id);

            if (auction is null)
                return NotFound();

            _adminService.StartAuctionFloorById(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Finish(int id)
        {
            var auction = _adminService.GetAuctionById(id);

            if (auction is null)
                return NotFound();

            _adminService.FinishAuctionFloorById(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Remove(int id)
        {
            var auction = _adminService.GetAuctionById(id);

            if (auction is null)
                return NotFound();

            _adminService.DeleteAuction(auction);
            return NoContent();
        }

        [HttpGet]
        public IActionResult Search(string search)
        {
            ViewData["search"] = search;

            var auctions = _adminService.ListAuctionsBySearch(search);
            return View("Index", auctions);
        }
    }
}
