using Alura.OnlineAuctions.WebApp.Data.Interfaces;
using Alura.OnlineAuctions.WebApp.Models;
using Alura.OnlineAuctions.WebApp.Services.Interfaces;

namespace Alura.OnlineAuctions.WebApp.Services.Handlers
{
    public class DefaultAdminService : IAdminService
    {
        private readonly IAuctionDao _auctionDao;
        private readonly ICategoryDao _categoryDao;

        public DefaultAdminService(IAuctionDao auctionDao, ICategoryDao categoryDao)
        {
            _auctionDao = auctionDao;
            _categoryDao = categoryDao;
        }

        public IList<Category> ListCategories()
        {
            return _categoryDao.GetAll();
        }

        public IList<Auction> ListAuctions()
        {
            return _auctionDao.GetAll();
        }

        public Auction? GetAuctionById(int id)
        {
            return _auctionDao.GetbyId(id);
        }

        public IList<Auction> ListAuctionsBySearch(string search)
        {
            return _auctionDao.SearchAuction(search);
        }

        public void InsertAuction(Auction auction)
        {
            _auctionDao.Insert(auction);
        }

        public void UpdateAuction(Auction auction)
        {
            _auctionDao.Update(auction);
        }

        public void DeleteAuction(Auction auction)
        {
            _auctionDao.Delete(auction);
        }

        public void StartAuctionFloorById(int id)
        {
            var auction = _auctionDao.GetbyId(id);
            if (auction is not null && auction.Status == AuctionStatus.Draft)
            {
                auction.Status = AuctionStatus.Floor;
                auction.Start = DateTime.Now;
                _auctionDao.Update(auction);
            }
        }

        public void FinishAuctionFloorById(int id)
        {
            var auction = _auctionDao.GetbyId(id);
            if (auction is not null && auction.Status == AuctionStatus.Floor)
            {
                auction.Status = AuctionStatus.Finished;
                auction.End = DateTime.Now;
                _auctionDao.Update(auction);
            }
        }
    }
}
