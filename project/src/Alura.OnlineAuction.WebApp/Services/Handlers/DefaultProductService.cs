using Alura.OnlineAuctions.WebApp.Data.Interfaces;
using Alura.OnlineAuctions.WebApp.Models;
using Alura.OnlineAuctions.WebApp.Services.Interfaces;

namespace Alura.OnlineAuctions.WebApp.Services.Handlers
{
    public class DefaultProductService : IProductService
    {
        private readonly IAuctionDao _auctionDao;
        private readonly ICategoryDao _categoryDao;

        public DefaultProductService(IAuctionDao auctionDao, ICategoryDao categoryDao)
        {
            _auctionDao = auctionDao;
            _categoryDao = categoryDao;
        }

        public IList<Auction> ListAuctionsBySearch(string search)
        {
            return _auctionDao.SearchAuction(search);
        }

        public IList<CategoryWithInfoAuction> ListCategoriesWithAuctions()
        {
            return _categoryDao
                .ListCategoriesWithAuctions()
                .Select(c => new CategoryWithInfoAuction
                {
                    Id = c.Id,
                    Description = c.Description,
                    Image = c.Image,
                    InDraft = c.Auctions.Where(l => l.Status == AuctionStatus.Draft).Count(),
                    InFloor = c.Auctions.Where(l => l.Status == AuctionStatus.Floor).Count(),
                    Finalized = c.Auctions.Where(l => l.Status == AuctionStatus.Finished).Count(),
                }).ToList();
        }

        public Category? GetCategoryWithAuctionsById(int id)
        {
            return _categoryDao.GetCategoryWithAuctionById(id);
        }
    }
}
