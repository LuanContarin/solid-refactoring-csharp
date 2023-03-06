using Alura.OnlineAuctions.WebApp.Models;
using Alura.OnlineAuctions.WebApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Alura.OnlineAuctions.WebApp.Controllers.API
{
    [ApiController]
    [Route("/api/auctions")]
    public sealed class AuctionApiController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AuctionApiController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet]
        public IActionResult EndpointGetLeiloes()
        {
            var auctions = _adminService.ListAuctions();
            return Ok(auctions);
        }

        [HttpGet("{id}")]
        public IActionResult EndpointGetAuctionById(int id)
        {
            var auction = _adminService.GetAuctionById(id);
            if (auction is null)
                return NotFound();

            return Ok(auction);
        }

        [HttpPost]
        public IActionResult EndpointPostAuction(Auction auction)
        {
            _adminService.InsertAuction(auction);
            return CreatedAtAction(nameof(EndpointGetAuctionById), auction);
        }

        [HttpPut]
        public IActionResult EndpointPutAuction(Auction auction)
        {
            _adminService.UpdateAuction(auction);
            return Ok(auction);
        }

        [HttpDelete("{id}")]
        public IActionResult EndpointDeleteAuction(int id)
        {
            var auction = _adminService.GetAuctionById(id);
            if (auction is null)
                return NotFound();

            _adminService.DeleteAuction(auction);
            return NoContent();
        }

        [HttpPost("{id}/floor")]
        public IActionResult EndpointStartFloor(int id)
        {
            var auction = _adminService.GetAuctionById(id);
            if (auction is null)
                return NotFound();

            _adminService.StartAuctionFloorById(id);
            return Ok();
        }

        [HttpDelete("{id}/pregao")]
        public IActionResult EndpointFinishFloor(int id)
        {
            var auction = _adminService.GetAuctionById(id);
            if (auction is null)
                return NotFound();

            _adminService.FinishAuctionFloorById(id);
            return Ok();
        }
    }
}
