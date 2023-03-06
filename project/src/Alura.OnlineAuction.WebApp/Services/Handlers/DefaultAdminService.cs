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
            return _categoryDao.ListCategories();
        }

        public IList<Auction> ListAuctions()
        {
            return _auctionDao.ListAuctions();
        }

        public Auction? GetAuctionById(int id)
        {
            return _auctionDao.GetAuctionById(id);
        }

        public IList<Auction> ListAuctionsBySearch(string search)
        {
            return _auctionDao.SearchAuction(search);
        }

        public void InsertAuction(Auction auction)
        {
            _auctionDao.InsertAuction(auction);
        }

        public void UpdateAuction(Auction auction)
        {
            _auctionDao.UpdateAuction(auction);
        }

        public void DeleteAuction(Auction auction)
        {
            _auctionDao.DeleteAuction(auction);
        }

        public void StartAuctionFloorById(int id)
        {
            var auction = _auctionDao.GetAuctionById(id);
            if (auction is not null && auction.Status == AuctionStatus.Draft)
            {
                auction.Status = AuctionStatus.Floor;
                auction.Start = DateTime.Now;
                _auctionDao.UpdateAuction(auction);
            }
        }

        public void FinishAuctionFloorById(int id)
        {
            var auction = _auctionDao.GetAuctionById(id);
            if (auction is not null && auction.Status == AuctionStatus.Floor)
            {
                auction.Status = AuctionStatus.Finished;
                auction.End = DateTime.Now;
                _auctionDao.UpdateAuction(auction);
            }
        }
    }
}
