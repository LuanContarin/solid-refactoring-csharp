using Alura.OnlineAuctions.WebApp.Data.Interfaces;
using Alura.OnlineAuctions.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace Alura.OnlineAuctions.WebApp.Controllers
{
    [ApiController]
    [Route("/api/leiloes")]
    public class LeilaoApiController : ControllerBase
    {
        private readonly IAuctionDao _auctionDao;
        private readonly ICategoryDao _categoryDao;

        public LeilaoApiController(IAuctionDao auctionDao, ICategoryDao categoryDao)
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
        public IActionResult EndpointGetLeilaoById(int id)
        {
            var auction = _auctionDao.GetAuctionById(id);
            if (auction is null)
                return NotFound();

            return Ok(auction);
        }

        [HttpPost]
        public IActionResult EndpointPostLeilao(Leilao auction)
        {
            _auctionDao.InsertAuction(auction);
            return CreatedAtAction(nameof(EndpointGetLeilaoById), auction);
        }

        [HttpPut]
        public IActionResult EndpointPutLeilao(Leilao auction)
        {
            _auctionDao.UpdateAuction(auction);
            return Ok(auction);
        }

        [HttpDelete("{id}")]
        public IActionResult EndpointDeleteLeilao(int id)
        {
            var auction = _auctionDao.GetAuctionById(id);
            if (auction is null)
                return NotFound();

            _auctionDao.DeleteAuction(auction);
            return NoContent();
        }
    }
}
