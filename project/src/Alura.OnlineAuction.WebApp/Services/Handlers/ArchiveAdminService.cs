using Alura.OnlineAuctions.WebApp.Data.Interfaces;
using Alura.OnlineAuctions.WebApp.Models;
using Alura.OnlineAuctions.WebApp.Services.Interfaces;

namespace Alura.OnlineAuctions.WebApp.Services.Handlers
{
    public class ArchiveAdminService : IAdminService
    {
        private readonly DefaultAdminService _defaultService;

        public ArchiveAdminService(IAuctionDao auctionDao, ICategoryDao categoryDao)
        {
            _defaultService = new(auctionDao, categoryDao);
        }

        public IList<Category> ListCategories()
        {
            return _defaultService.ListCategories();
        }

        public IList<Auction> ListAuctions()
        {
            return _defaultService
                .ListAuctions()
                .Where(x => x.Status != AuctionStatus.Archived)
                .ToList();
        }

        public Auction? GetAuctionById(int id)
        {
            return _defaultService.GetAuctionById(id);
        }

        public IList<Auction> ListAuctionsBySearch(string search)
        {
            return _defaultService
                .ListAuctionsBySearch(search)
                .Where(x => x.Status != AuctionStatus.Archived)
                .ToList();
        }

        public void InsertAuction(Auction auction)
        {
            _defaultService.InsertAuction(auction);
        }

        public void UpdateAuction(Auction auction)
        {
            _defaultService.UpdateAuction(auction);
        }

        public void DeleteAuction(Auction auction)
        {
            if (auction is not null && auction.Status != AuctionStatus.Floor)
            {
                auction.Status = AuctionStatus.Archived;
                _defaultService.UpdateAuction(auction);
            }
        }

        public void StartAuctionFloorById(int id)
        {
            _defaultService.StartAuctionFloorById(id);
        }

        public void FinishAuctionFloorById(int id)
        {
            _defaultService.FinishAuctionFloorById(id);
        }
    }
}
