using Alura.OnlineAuctions.WebApp.Data.Interfaces;
using Alura.OnlineAuctions.WebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace Alura.OnlineAuctions.WebApp.Data.EfCore
{
    public sealed class AuctionDao : IAuctionDao
    {
        private readonly AppDbContext _dbContext;

        public AuctionDao(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IList<Auction> ListAuctions()
        {
            return _dbContext.Auctions
                .Include(x => x.Category)
                .ToList();
        }

        public IList<Auction> SearchAuction(string search)
        {
            return _dbContext.Auctions
                .Where(l => string.IsNullOrWhiteSpace(search)
                            || l.Title.ToUpper().Contains(search.ToUpper())
                            || l.Description.ToUpper().Contains(search.ToUpper())
                            || l.Category.Description.ToUpper().Contains(search.ToUpper()))
                .ToList();
        }

        public Auction? GetAuctionById(int id)
        {
            return _dbContext.Auctions.Find(id);
        }

        public void InsertAuction(Auction auction)
        {
            _dbContext.Auctions.Add(auction);
            _dbContext.SaveChanges();
        }

        public void UpdateAuction(Auction auction)
        {
            _dbContext.Auctions.Update(auction);
            _dbContext.SaveChanges();
        }

        public void DeleteAuction(Auction auction)
        {
            _dbContext.Auctions.Remove(auction);
            _dbContext.SaveChanges();
        }
    }
}
