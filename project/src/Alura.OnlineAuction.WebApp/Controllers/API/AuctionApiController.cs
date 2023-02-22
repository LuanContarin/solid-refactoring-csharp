using Alura.OnlineAuctions.WebApp.Data.Interfaces;
using Alura.OnlineAuctions.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace Alura.OnlineAuctions.WebApp.Controllers.API
{
    [ApiController]
    [Route("/api/auctions")]
    public sealed class AuctionApiController : ControllerBase
    {
        private readonly IAuctionDao _auctionDao;
        private readonly ICategoryDao _categoryDao;

        public AuctionApiController(IAuctionDao auctionDao, ICategoryDao categoryDao)
        {
            _auctionDao = auctionDao;
            _categoryDao = categoryDao;
        }

        [HttpGet]
        public IActionResult EndpointGetLeiloes()
        {
            var auctions = _auctionDao.ListAuctions();
            return Ok(auctions);
        }

        [HttpGet("{id}")]
        public IActionResult EndpointGetAuctionById(int id)
        {
            var auction = _auctionDao.GetAuctionById(id);
            if (auction is null)
                return NotFound();

            return Ok(auction);
        }

        [HttpPost]
        public IActionResult EndpointPostAuction(Auction auction)
        {
            _auctionDao.InsertAuction(auction);
            return CreatedAtAction(nameof(EndpointGetAuctionById), auction);
        }

        [HttpPut]
        public IActionResult EndpointPutAuction(Auction auction)
        {
            _auctionDao.UpdateAuction(auction);
            return Ok(auction);
        }

        [HttpDelete("{id}")]
        public IActionResult EndpointDeleteAuction(int id)
        {
            var auction = _auctionDao.GetAuctionById(id);
            if (auction is null)
                return NotFound();

            _auctionDao.DeleteAuction(auction);
            return NoContent();
        }
    }
}
