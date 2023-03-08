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

        public IList<Auction> SearchAuction(string search)
        {
            return _dbContext.Auctions
                .Where(l => string.IsNullOrWhiteSpace(search)
                            || l.Title.ToUpper().Contains(search.ToUpper())
                            || l.Description.ToUpper().Contains(search.ToUpper())
                            || l.Category.Description.ToUpper().Contains(search.ToUpper()))
                .ToList();
        }

        public IList<Auction> GetAll()
        {
            return _dbContext.Auctions
                .Include(x => x.Category)
                .ToList();
        }

        public Auction? GetbyId(int id)
        {
            return _dbContext.Auctions.Find(id);
        }

        public void Insert(Auction entity)
        {
            _dbContext.Auctions.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Update(Auction entity)
        {
            _dbContext.Auctions.Update(entity);
            _dbContext.SaveChanges();
        }

        public void Delete(Auction entity)
        {
            _dbContext.Auctions.Remove(entity);
            _dbContext.SaveChanges();
        }
    }
}
